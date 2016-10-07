DocsByReflection
================
[![Build status](https://ci.appveyor.com/api/projects/status/fsi87lngik22t534?svg=true)](https://ci.appveyor.com/project/giacomelli/docsbyreflection)

Discover the code documentation at runtime by reflection. 

Original source code from Jim Blackler' DocsByReflection: http://jimblackler.net/blog/?p=49

Setup
========

NuGet
------
PM> Install-Package DocsByReflection


Using
========
```c#

// From type.
var typeDoc = DocsService.GetXmlFromType(typeof(Stub));

// From property.
var propertyInfo = typeof(Stub).GetProperty("PropertyWithDoc");
var propertyDoc = DocsService.GetXmlFromMember(propertyInfo);

// From method.
var methodInfo = typeof(Stub).GetMethod("MethodWithGenericParameter");
var methodDoc = DocsService.GetXmlFromMember(methodInfo);

// From assembly.
var assemblyDoc = DocsService.GetXmlFromAssembly(typeof(Stub).Assembly);

```

### ThrowError parameter
*All of the above methods have a last parameter called throwError. The default parameter's value is true, when value is true any documentation not found will throw an error, when value is false the method will return a null value.*


License
========
DocsByReflection is release using a dual-license. You can choose the one that best fit your needs:

- [MIT](http://opensource.org/licenses/MIT)
- [LGPL](http://opensource.org/licenses/LGPL-3.0)
