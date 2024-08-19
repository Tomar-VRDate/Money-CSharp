# [A Money type for the CLR](https://www.codeproject.com/Articles/28244/A-Money-type-for-the-CLR)
## Introduction
A large number of the world's applications manipulate `Money` values. Here is a convenient, high-performance `Money` structure for the CLR which handles arithmetic operations, `Currency` types, formatting, and careful distribution and rounding without loss.

* [Project home](http://www.codeplex.com/MoneyType)
## Background
The CLR doesn't include a native `Money` type. This could be seen as a shortcoming, or a reasonable design decision, given that it is an object-oriented framework, and adding your own type to encapsulate the data and behavior you need is pretty much its reason of being.

`Money`, however, is a type which is so primitive, and so pervasive, that its absence is notable. Other primitive types are built-in, after all. Martin Fowler considers this in Patterns of Enterprise Application Architecture [PEAA]:

A large proportion of the computers in this world manipulate `Money`, so it's always puzzled me that `Money` isn't actually a first class data type in any mainstream programming language. The lack of a type causes problems, the most obvious surrounding currencies. ... The good thing about object-oriented programming is that you can fix these problems by creating a `Money` class that handles them. Of course, it's still surprising that none of the mainstream base class libraries actually do this. (p. 488)

## Using the Code
Using this `Money` type is easy: it works just like any other numeric type.
```
Money m1 = 1.25;
Money m2 = 0.75;

Money total = m1 + m2;
Money difference = m1 - m2;
```
You can create `Money` instances with a different `Currency` than your current culture's `Currency`:
```
Money m3 = new Money(1.25, Currency.Eur);
```
...but you can't perform operations on two instances with different currency types:
throws an InvalidOperationException due to different currencies
```
total = m2 + m3;
```
To distribute `Money` without losing fractions, use a `MoneyDistributor`:
```
Money amountToDistribute = 0.05M;

// two decimal places
MoneyDistributor distributor = new MoneyDistributor(amountToDistribute,
FractionReceivers.LastToFirst,
RoundingPlaces.Two);
Money[] distribution = distributor.Distribute(0.3M);

Assert.Equal(3, distribution.Length);
Assert.Equal(new Money(0.01M), distribution[0]);
Assert.Equal(new Money(0.02M), distribution[1]);
Assert.Equal(new Money(0.02M), distribution[2]);
```
That's all there is using it! However, if you are interested in a good case-study of the inputs, rationale, and struggles of a type design, read on...

## Analysis of Approaches to a `Money` Type for the CLR
## Approach in this Implementation
## `Money`
In [PEAA], Martin Fowler and Matt Foemmel's example shows `Money` as an object with value semantics. In the CLR, a value type is a first-class entity, and so it makes sense to combine these two and make `Money` a value type. This should also help deal with the issue of performance the authors bring up, since value types are not reference counted on the heap, meaning less pressure on the GC and therefore higher performance.

Also in [PEAA], the underlying type used to store the value was an [Int64](http://msdn.microsoft.com/library/system.int64.aspx) (long). The authors then scale the value by some power of 10 (0 - 3) to represent fractional units. In this type, I kept the integral value storage, but opted to represent the fraction as a completely separate [Int32](http://msdn.microsoft.com/library/system.int32.aspx), which is scaled by 10^9 (the largest power of 10 which fits into an [Int32](http://msdn.microsoft.com/library/system.int32.aspx)). This allows storing much smaller fractions, which is useful for intermediate computations. Due to this, the type is a fixed-decimal point numeric representation. This is how relational databases often store `Money`, so it makes a natural fit. Another alternative was to represent the `Money` value with a [System.Decimal](http://msdn.microsoft.com/library/system.decimal.aspx). The problem with this approach is that [System.Decimal](http://msdn.microsoft.com/library/system.decimal.aspx) is a binary floating-point type, and binary floating-point types give us all sorts of headaches when computing with them and not treating the round-off or computational error accumulation with extreme care. These issues can be avoided by working with whole numbers and scaling to represent fractions.

I opted to separate the responsibility to allocate `Money` in a defined distribution into another class: `MoneyDistributor`. The reasons for this are that I think it helps readability and conscientious use, and having both a divide operation and an allocate operation on the `Money` class seemed to be a bit of a conflict of interest. By separating out the responsibility for not losing (or gaining!) fractions of `Money` into a separate class, I force `Money` to admit that it can't really do a good job at dividing itself up, and rather entrusts this to another class. Further, this then allows subclassing of `MoneyDistributor` for custom behavior, which can no longer be done with `Money`, since it is a value type.

## `Currency`
With regard to the `Currency` aspect of `Money`, the CLR doesn't have this as an available type. Java follows ISO 4217, and so it is pretty safe to add something like this - just follow the spec. My first inclination was to just make a `Currency` class with static fields: one for each `Currency`. However, Jason Hunt's implementation used a CultureInfo instance to represent the `Currency`, and it got me to think about how this is already somewhat present in the BCL. Since the CultureInfo classes are built around an [Int32](http://msdn.microsoft.com/library/system.int32.aspx) identifier known as the LCID, it seems reasonable to use this as the `Currency` identifier as well. However, after some observation, this turns out not to be a sound approach: not only are the culture-to-`Currency` relationships not 1-to-1, but they are also not N-to-1, since some cultures use more than one `Currency`. In the end, I used the ISO spec to generate some code to load lookup tables and keep everything related based on the ISO numeric code for any given `Currency`. This code is an [Int32](http://msdn.microsoft.com/library/system.int32.aspx), and this serves as the only field in a `Currency` instance, allowing me to represent the `Currency` as a value type as well. This makes serialization of a `Money` value quite simple: there are no reference fields in it! One last functionality of `Currency` to point out: IFormatProvider is implemented on it, so that when ToString() is called on `Money`, the associated `Currency` instance is passed with it and it gets formatted as expected.

## Other Approaches
When I started working on this type, I didn't find an implementation of a money type for the CLR. Two things of note: the first is that I didn't quite search for the right keywords (apparently, no one calls the CLR by its name: "the CLR"; everyone refers to it in a roundabout way using "C#" or ".NET"), and the second is that a good implementation was published the same day completely independently, half way around the world! Let's compare these other implementations, with the goal of finding the best approach if one clearly exists.

| | [Value Type or Reference Type](#ValueVsReferenceType) | [Currency type](#CurrencyType) | [Supports ISO 4217](#SupportsIso4217) | [Fixed or floating](#FixedVsFloating) | [Handles distribution](#Distribution) | [Arithmetic operations](#ArithmeticOps) | [Handles formatting](#HandlesFormatting) | [Currency instance formats the value](#CurrencyFormatsMoney) | [Parses formatted strings](#Parses) |
|----------------------------------------------------------------------------------------------|-----------|---|---|----------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------------|---|---|---|---|
| [Jason Hunt's](http://www.noticeablydifferent.com/CodeSamples/Money.aspx)                    | Reference | CultureInfo / RegionInfo | No | Floating                                                                                                 | No                                                                                                       | Yes | Yes* | No | No |
| [Michael R. Brumm's](http://www.michaelbrumm.com/money.html)                                 | Value     | Enum/ table lookup | No | Floating | No                                                                                                       | No                                                                                                       | Yes* | No | Yes |
| [chimeric69's](http://www.codeproject.com/KB/vb/moneyDatatype.aspx)                          | Value     | CultureInfo / RegionInfo | No | Floating                                                                                                 | No                                                                                                       | Yes | Yes | No | Yes |
| [Pascal Lindelauf's](http://thinkarchitecture.blogspot.com/2008/07/making-money-with-c.html) | Reference | Custom reference type / Enum | Yes | Floating                                                                                                 | Yes                                                                                                      | Yes | Yes* | No | No |
| [This implementation](https://www.codeproject.com/Articles/28244/A-Money-type-for-the-CLR)   | Value     | Custom value type / table lookup | Yes | Fixed                                                                                                    | Yes                                                                                                      | Yes | Yes | Yes | No |
* (not as `IFormattable`)

## Value vs. Reference Type
Given that value types are first-class in the CLR, it seems quite natural to use it as the basis for a `Money` type. A given instance of `Money` is considered equal to another instance when the values are equal. This is known as having value semantics. Fowler calls types which have this characteristic ["value-objects"](https://martinfowler.com/eaaCatalog/valueObject.html). On the other hand, you give up certain flexibility when defining it to be a value type vs. a reference type, namely: inheritance. This can get to be a problem if you want to keep your `Money` allocation/distribution code with the `Money` type and then modify it in a subclass, or if your database mapping layer depends on a reference type to wrap with database mapping code. I've handled the first concern by delegating the distribution responsibility to a separate class, and the second concern might indicate that it's time to get a more robust mapping layer. Given that a value type is intrinsically serializable, it shouldn't be hard at all to store this type in a database. There are some variations on how to do this depending on whether you are using a floating point or fixed point format for the value. More on this in "Fixed or floating".

## `Currency` Type
The major decision here is to determine if a custom type is used, or a combination, or `System.Globalization.CultureInfo`/`System.Globalization.RegionInfo` to represent the `Currency`, and, if a custom type is used, if it should be a full-fledged type or an enumeration. Although the CultureInfo/RegionInfo approach is immediately appealing, it leaves some ambiguities which are hard to resolve, since some regions use more than one `Currency`, and various currencies are used in more than one region. The other drawback is that aspects of the ISO 4217 spec which covers `Currency` are not directly accessible: notably the numeric code and the exponent. The custom type approach allows you to deal with this, but the enumeration approach leaves you with the need to lookup this information in a table or via some static accessor method. These could be encapsulated in the `Money` type so users wouldn't need to handle the specifics outside of just the enumeration. The custom type approach seems to me to be the most sound, since there is a good amount of data to encapsulate (see "Supports ISO 4217"). The actual type could be a reference type, but since the data rarely changes, and is keyed by a numeric code, a value type with that code as the key into static lookup tables is efficient and allows the value to be embedded in the `Money` type, making `Money` values very portable. By building these static lookup tables from the ISO spec as well as enumerating CultureInfo/RegionInfo, you can have all the information you need about `Currency` from a single [Int32](http://msdn.microsoft.com/library/system.int32.aspx) encapsulated in a value type.

## Supports ISO 4217
The world standard which governs currencies will have an impact on any `Currency` type design. Or at least it should. If `CultureInfo`/`RegionInfo` is used, this spec is indirectly observed. However, as pointed out in "Currency type", there are ambiguities when using CultureInfo/RegionInfo to represent `Currency`, and implementing the spec fully in its own type resolves these. There are several components to each `Currency` covered in the spec: `Currency` name, a symbol, an exponent to indicate the smallest generally used division of the `Currency`, a three letter code, and a numeric code. It is this extra information which serves as a major factor of how to represent `Currency` as a custom type, as noted more completely in "Currency type".

## Handles Distribution Without Loss
Allocating funds over a number of divisions or distributions can lead to fractions of units being gained or lost, which can then be magnified by subsequent computation or storage and retrieval. People who handle `Money` don't like this. In [PEAA], allocation is handled by picking the number of digits after the decimal to keep, and truncating the quotient of the division at that digit, then subtracting the sum of all quotients from the initial amount, and distributing the remainder among the quotients in smallest-decimal increments. The modification I make here from that text is the inclusion of the precision desired to truncate to (from 0 - 9 places after the decimal) as well as an enumeration of three methods of distribution: first-to-last, last-to-first, and random.

## Fixed or Floating
Here's a question the answer to which sure to raise some eyebrows to those who haven't a firm grasp on the consequences of using floating-point types. If you've dealt with binary floating point numbers with fractions that cannot be represented exactly in base 2, but can be represented exactly in base 10, you're going to lose `Money`, and probably a lot more than just the fractions of `Currency` units that it appears to be at first. That's why, for a `Money` type, it has to be a fixed format which is stored in base 10. By way of example, 1/3 is not exactly representable in base 10 or base 2. In base 10, a common way to split a unit three ways is to take the fractional units and give them to the last distribution, e.g., $1 three ways would be: $0.33, $0.33, and $0.34. Both 0.33 and 0.34 are representable in base 10; however, they are not representable exactly as a finite-length base 2 number, and some precision will be lost when doing so. This loss is often compounded or magnified during operations. There is really only one right answer here: fixed base 10.

Another benefit of choosing a fixed base 10 number: most relational databases have an exact numeric type which stores the value in base 10, so no loss will be incurred when storing and retrieving these values from a database. This will not always be true with base 2 numbers, and database mapping code will need to account for this if they are used instead. If your database doesn't have a base 10 number to store the `Money` in, or it is inadequate, developing a custom strategy using 12 bytes (a long and an int) or 16 bytes with `Currency` (a long and two ints) is straightforward.

## Arithmetic Operations
In the CLR, you are able to define operators on a custom type. Given that `Money` is involved in plenty of arithmetical operations, it is natural to do so. The only ones which should be suspected are multiplication and division, since using these to get distributions of `Money` which need to add up to a specific total (or starting amount) without any loss or gain is not generally possible. [PEAA] uses an "allocate" method on the `Money` type to replace division in these cases. A decision can be made on whether to keep this method with the `Money` type or delegate it to a specialized class. The first method means that you don't need to know about a separate type, and most good IDEs will show you the allocate method and a keen developer should notice it, but that there is a bit of tension in the `Money` class on how and when to divide itself. And the second method means that you have a clearly defined responsibility, code which uses it standing out a bit more as something special going on, and the ability to subclass for custom behavior (assuming that `Money` is a value type), but the need to discover and learn about a second class. I prefer the separate class: it could be argued to be purely style, but if a distribution behavior change is needed (although when pressed, I couldn't admit to knowing when this would be needed), this approach would make it much cleaner.

## Handles Formatting
The CLR has a pattern in place to support formatting of a type to a string representation: `IFormattable`. It makes good sense to support this interface and match developer expectations on how `ToString()` should work.

## `Currency` Instance Formats `Money` Values
Following on supporting `IFormattable`, there is a standard formatting string to indicate that the value should be formatted as `Currency`: "C". In order to support this universally, you can pass into `IFormattable.ToString()` or `String.Format()` a format provider which will be used to help format the type correctly. Using a `Currency` instance as the `IFormattable` instance also seems natural. The only problem with this is a confusion which might arise when using a different `Currency` than what the `Money` value is expressed in: if the `Currency` isn't checked and an exception thrown on a difference, a `Money` value will be expressed in a different `Currency` but with the original `Currency`'s numeric value. Some kind of `Currency` conversion might need to be done instead, but this appears too complex and unwieldy to me.

## Parses Formatted Strings
The inverse of formatting: parsing a string into an instance of `Money` and related `Currency`. No interface provides `Parse()` and (in 2.0 and on) `TryParse()`, but they are found as static methods on the BCL's primitive types. Seeing as we'd like to make our `Money` type as similar to BCL primitive types as we can, in order to create the illusion that it is part of the BCL, implementing these two methods becomes part of our job.

## Source Update History & Notes
* 2024-08-19: @VRDate Updated the project to VS2022, Created README.md from
  [codekaizen](https://www.codeproject.com/script/Membership/View.aspx?mid=91332) [A Money type for the CLR](https://www.codeproject.com/Articles/28244/A-Money-type-for-the-CLR), renamed Classes, Added ReginalInfo Maps, Expose read only lookup maps, updated Unit Tests, moved `USD` as first `CurrencyEntry` to default for `$`
* 2013-03-18: [Download soure code - 137.96 KB](https://www.codeproject.com/KB/recipes/MoneyTypeForCLR/MoneyType.zip) Updated the project to VS2012, implemented rounding on `Money`, implemented IComparible on `Money`, added `Money` extension methods to distribute without needing to create a `MoneyDistributor` instance, added TryParse static method on `Money` and `Currency`, added debugger visualization to `Money`
* 2008-08-01: Source updated
* 2008-07-30: First version.

## License
This article, along with any associated source code and files, is licensed under [The Microsoft Public License (Ms-PL)](http://www.opensource.org/licenses/ms-pl.html)