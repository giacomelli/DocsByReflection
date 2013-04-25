DocsByReflection
================

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


License
========
Licensed under the [LGPL (GNU LESSER GENERAL PUBLIC LICENSE)](http://www.gnu.org/licenses/lgpl-3.0-standalone.html).

Change Log
========
1.0.0 Initial version.