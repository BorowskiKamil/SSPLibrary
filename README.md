# SSPLibrary (stands for Sorting, Searching, Paging Library)

SSPLibrary is a simple extension which provides a set of methods to sort, filter and page API's results.

During my development experience I struggled many times with creating a project from scratch so I had to code functionalities which I've done plenty of times and got sick of it. So I got the idea to create a library with the one of every API's basic functionalities - paging, sorting and filtering. This is my first library, so I've learned a lot during the development process. Also, it's an alpha version so I don't recommend to use it in the production. 

It supports only ASP.NET Core and should work with every ORM that is compatible with .NET Core.

## Available packages  

Name               | Package name                              | Current version (master branch)
-----------------------|-------------------------------------------|-----------------------------
Core             | `SSPLibrary` | [![NuGet](https://img.shields.io/nuget/v/SSPLibrary.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/SSPLibrary/)
EFCore Extension                 | `SSPLibrary.Extensions.Microsoft.EntityFrameworkCore`    | [![NuGet](https://img.shields.io/nuget/v/SSPLibrary.Extensions.Microsoft.EntityFrameworkCore.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/SSPLibrary.Extensions.Microsoft.EntityFrameworkCore/)

## How to get started

### Mvc configuration

Firstly, append a SSPLibrary to MvcBuilder in Startup.cs file:

```csharp
services.AddMvc().AddSSP(options => 
{
	options.PagingOptions.DefaultLimit = 5;
});
```

You can also change default options like in the example above.

### Sortable i Searchable attributes in Dto

Specify in your model class which properties can be filtred or by which ones you can use sorting. You can do this by adding an attribute above the property:

```csharp
[Sortable(Default = true, WhenDefaultIsDescending = true)]
public int Id { get; set; }

[Searchable]
public string Name { get; set; }

[Searchable]
public bool IsDone { get; set; }
```

### Controller configuration

Add an QueryParameters<TEntity> to method signature, where TEntity is your Dto type. Library will automatically parse query. You can also validate it by simply adding model state validation - you can see it in example below:

```csharp
[HttpGet(Name = nameof(GetAllTasks))]
public IActionResult GetAllTasks(QueryParameters<TodoTask> queryParameters)
{
	if (!ModelState.IsValid) return BadRequest(ModelState);

	var result = _repository.GetTasks(queryParameters);
	return Ok(result.ToPagedCollection(queryParameters));
}
```

As you saw I performed a ```ToPagedCollection()``` method to extend the result to paging links and other usesful data.

### Applying paging, sorting and filtering

Probably you are wondering what happend in ```GetTasks()``` method. Well, depending on your needs, you can apply different set of SSP's functions.

```csharp
public PagedResults<TodoTask> GetTasks(QueryParameters<TodoTask> queryParameters)
{
	var entities = GenerateFakeTasks()
			.AsQueryable()
			.ApplySearching(queryParameters)
			.ApplySorting(queryParameters)
			.ToPagedResults(queryParameters);

	return entities;
}
```

```GenerateFakeTasks()``` returns a list of tasks and as you can see I can apply every SSP's function seperetly which gives me a freedom of action.


## Query Parameters

### Paging

Paging parameters in query are optional - if you don't give any, the library will take the default ones. You can configurate the default options in ```AddSSP()``` method - what I showed in "Mvc configuration" section.

**Paging parameters:**

Name               | Description
-----------------------|------------------------------------------------------------------------
Offset             | How much of results should be skipped
Limit                 | Limit of results

**Example:** 
>```GET /api/Tasks?limit=5&offset=10```

### Sorting

There is one parameter - **orderBy** - just put a name of property in value and the library will sort it. It can be sort descending by simply putting a dash (**-**) before the property name. Also, it's posible to sort by many properties by dividing names with comma (**,**).

Remember, you can sort only these properties which has a **sortable attribute**.

**Example:** 
>```GET /api/Tasks?limit=5&offset=10&orderby=Name,-Id```

### Searching/Filtering

As in the sorting - you can only search using properties with a **searchable attribute**. The searching parameter key is - **search** and in a value put a name of the property, an operator and a searching value.

**Supported operators:**

Operator               | Description							| Allowed Type
-----------------------|----------------------------------------|--------------------------------
==             | Equal | Object
==*            | Case Insensitive Equal | String
!=             | Not Equal | Object
!=*            | Case Insensitive Not Equal | String
?              | Contains | String
?*             | Case Insensitive Contains | String
^              | Starts With | String
^*             | Case Insensitive Starts With | String
$              | Ends With | String
$*             | Case Insensitive Ends With | String
\>              | Greather Than | Number, DateTime, DateTimeOffset
<              | Less Than | Number, DateTime, DateTimeOffset
\>=             | Greater Than Or Equal | Number, DateTime, DateTimeOffset
<=             | Less Than Or Equal | Number, DateTime, DateTimeOffset

**Examples:** 
>```GET /api/Tasks?search=Name==Task 49|Task 32|Task 22```\
>```GET /api/Tasks?search=CreatedAt<2018-07-23```\
>```GET /api/Tasks?search=IsDone==true```