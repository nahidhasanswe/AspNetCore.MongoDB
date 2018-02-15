# AspNetCore.MongoDB
MongoDB CRUD operation in AspNetCore 2

# Nuget Package
https://www.nuget.org/packages/AspNetCore.MongoDB

# How to use
You have to add the following lines to your Startup.cs in the ASP.NET Core Project.
```C#
public void ConfigureServices(IServiceCollection services)
{
            services
                .Configure<MongoDBOption>(Configuration.GetSection("MongoDBOption"))
                .AddMongoDatabase();

}
```

In appsettings.json you can configure the correct Path with a new section to your MongoDB instance.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=xxxx"
  },
  "Logging": {
    "IncludeScopes": false,
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "MongoDBOption": {
    "ConnectionString": "mongodb://localhost:27017",
    "Database": "AspNetCoreIdentity"
  }
}

```

Now you have to write your own enity for MongoDB Operation. Your Entity must inherited from `IMongoEntity`. IMongoEntity contain Bson id, CreatedBy, CreatedDate, UpdatedDate, UpdatedBy, isRemoved. So you will not includ these property to your enity. Your Entity will be looked like as below

```c#
public class SampleModel : IMongoEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
```

[N.B] : Remember that your Entity Name will be the MongoDB Collection Name.

Now you have to Inject IMongoOperation with your Entity looks as below :

```C#
public class SampleController : Controller
    {
        private readonly IMongoOperation<SampleModel> _operation;

        public SampleController(IMongoOperation<SampleModel> operation)
        {
            _operation = operation;
        }
    }
```

Now you will able to CRUD operation against the Entity to your Controller as below

```C#
	public async Task Save(SampleModel model)
	{
		await _operation.SaveAsync(model);
	}

	public async Task Update(SampleModel model)
	{
		await _operation.UpdateAsync(model.Id, model);
	}

	public async Task<IEnumerable<SampleModel>> GetAll()
	{
		return await _operation.GetAllAsync();
	}

	public async Task<SampleModel> GetById(string Id)
	{
		return await _operation.GetByIdAsync(Id);
	}

	public async Task<DeleteResult> RemoveOne(string id)
	{
		return await _operation.RemoveByIdAsync(id);
	}

	public async Task<DeleteResult> RemoveAll()
	{
		return await _operation.RemoveAllAsync();
	}

```
# Queryable Data
If you need to `IQueryable` data for entity, just do like as below:

```C#
public  IQueryable<SampleModel> GetAsIQueryabale()
        {
            return _operation.GetQuerableAsync();
        }
```

If you need to manually operation of your Mongo Collection or Table then just get the `IMonogoCollection` and perform your activity to the collection as below
```C#
public IMongoCollection<SampleModel> GetCollection()
        {
            return _operation.GetMongoCollection();
        }
```

## Contact Me
If you fetch any problem to implementing. Please ask me anytime through mail `nahidh527@gmail.com` and create a issue. Thanks for using this.