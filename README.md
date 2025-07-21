🛒 ASP.NET Core E-commerce Web Application (In Progress)
This is a work-in-progress E-commerce web app built with ASP.NET Core 8 using Clean Architecture. It aims to support product management, cart, orders, and more.

🔧 Tech Stack
-ASP.NET Core 8 (MVC + Web API)
-Entity Framework Core
-SQL Server
-Identity Framework
-Clean Architecture (Domain, Application, Infrastructure, Web)

📁 Project Structure
AspNetCoreEcommerceSln.sln
│
└───src
    ├── eCommerce.Web                 → UI Layer (Controllers, Views)
    ├── eCommerce.Application         → Application Logic (DTOs, Services)
    ├── eCommerce.Domain              → Entities & Business Rules
    └── eCommerce.Infrastructure      → EF Core, Repositories
    │
└───tests
    ├── eCommerce.UnitTest 

🛍️ E-Commerce Web App – Features (Planned + Existing)

👤 User & Account
-User registration/login (ASP.NET Identity)
-Roles: Admin, Seller, Customer
-Profile management
-Address management
-Wishlist functionality
-Notifications

🛒 Shopping & Orders
-Add to cart & manage cart items
-Place orders
-Order history & status tracking
-Cancel order within 24h
-Product Q&A
-Feedback & reviews (with images)

📦 Product Management
-Product creation (attributes, variants, images)
-Product features & configurations
-Product discount system
-Category & subcategory support
-Brand management
-Feature-category linking
-Inventory control per product

📤 Seller Panel
-Supplier & product management
-Supplier transactions
-Warehouse & stock tracking

💰 Transactions
-Payment handling
-Refund requests
-Return requests

🎯 Marketing & UX
-Homepage banners
-Product category browsing
-Brand filtering
-Featured products

📈 Admin Features
-Audit logs
-Role & permission management
-Feedback moderation

🚀 How to Run
1. Clone the repo
2. Set up DB connection in appsettings.json
3. Run migrations
4. Launch the project
