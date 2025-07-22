## 🌐 Controller Structure & Naming (ASP.NET Core)

### ✅ Naming
- Controller name = Entity + `Controller`\
  -> `ProductsController`\
  -> `BrandsController`\
  -> `UsersController`

### 🧠 Responsibilities
- Handle HTTP requests (GET/POST)
- Return Views, Redirects, or Partial Views
- No business logic here — delegate to Application Services
- Manage:\
  -> Routing\
  -> ViewModel Binding\
  -> TempData / ViewData / ViewBag\
  -> ModelState validation
 
### 🧱 Recommended Action Method Order

```csharp
public IActionResult Index()
public IActionResult Details(Guid id)
public IActionResult Create()
[HttpPost] public IActionResult Create(ViewModel model)
public IActionResult Edit(Guid id)
[HttpPost] public IActionResult Edit(ViewModel model)
public IActionResult Delete(Guid id)
```


## 🧠 Method Naming Conventions

---

### 🔧 **Service Layer (Business Logic)**

| Action     | Method Name               |
|------------|---------------------------|
| Create     | `CreateEntityAsync`       |
| Update     | `UpdateEntityAsync`       |
| Delete     | `DeleteEntityAsync`       |
| Read (One) | `GetEntityByIdAsync`      |
| Read (All) | `GetAllEntitiesAsync`     |

---

### 🗃️ **Repository Layer (Data Access)**

| Action     | Method Name             |
|------------|-------------------------|
| Create     | `InsertAsync`           |
| Update     | `UpdateAsync`           |
| Delete     | `DeleteByIdAsync`       |
| Read (One) | `FindByIdAsync`         |
| Read (All) | `FetchAllAsync`         |


## **Common Method Names in Unit of Work Layer**

---

### ✅ **Create**
- `AddBrandAsync_ShouldReturnBrandId_WhenBrandIsValid`
- `AddBrandAsync_ShouldThrowException_WhenBrandIsInvalid`

---

### 📖 **Read**
- `GetBrandByIdAsync_ShouldReturnBrand_WhenIdIsValid`
- `GetBrandByIdAsync_ShouldReturnNull_WhenBrandNotFound`
- `GetAllBrandsAsync_ShouldReturnListOfBrands`

---

### ✏️ **Update**
- `UpdateBrandAsync_ShouldReturnTrue_WhenUpdateIsSuccessful`
- `UpdateBrandAsync_ShouldThrowException_WhenBrandDoesNotExist`

---

### ❌ **Delete**
- `DeleteBrandAsync_ShouldReturnTrue_WhenDeleteIsSuccessful`
- `DeleteBrandAsync_ShouldReturnFalse_WhenBrandNotFound`

