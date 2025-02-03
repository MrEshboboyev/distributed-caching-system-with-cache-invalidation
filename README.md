# 🚀 Distributed Caching System with Cache Invalidation  

![.NET 9](https://img.shields.io/badge/.NET%209-blue?style=for-the-badge)
![Redis](https://img.shields.io/badge/Redis-%F0%9F%93%9A-red?style=for-the-badge)
![High Performance](https://img.shields.io/badge/High%20Performance-%E2%9C%85-green?style=for-the-badge)
![Caching](https://img.shields.io/badge/Caching-%F0%9F%94%92-yellow?style=for-the-badge)
![Clean Architecture](https://img.shields.io/badge/Clean%20Architecture-%F0%9F%9A%80-purple?style=for-the-badge)

A **high-performance distributed caching system** built with **.NET 9** and **Redis**, featuring **cache invalidation, multiple caching strategies (Cache-Aside, Write-Through), and Clean Architecture**. This project demonstrates how to optimize application speed and scalability using caching.  

---

## 🔥 Key Features  

✅ **Distributed Caching** – Uses **Redis** for low-latency, high-speed caching.  
✅ **Cache Invalidation** – Ensures **data consistency** with real-time updates.  
✅ **Multiple Caching Strategies**:  
   - **Cache-Aside (Lazy Loading)** – Loads data only when needed.  
   - **Write-Through** – Ensures cache and database consistency.  
✅ **Event-Driven Design** – Uses **Redis Pub/Sub** for efficient cache invalidation.  
✅ **Scalable & High-Performance** – Optimized for cloud & microservices.  
✅ **Clean Architecture** – Ensures maintainability and modularity.  

---

## 🛠️ Technologies Used  

🔹 **.NET 9** – Latest .NET version for cutting-edge performance.  
🔹 **Redis** – Distributed caching system for low-latency storage.  
🔹 **StackExchange.Redis** – .NET Redis client for caching.  
🔹 **Entity Framework Core** – Database interactions & caching logic.  
🔹 **CQRS + MediatR** – Implements query-command separation.  
🔹 **Serilog** – Centralized logging for monitoring & observability.  
🔹 **Docker** – Containerized Redis for easy setup.  

---

## 🏛️ Architecture  

This project follows **Clean Architecture** with **domain-driven principles**:  

📌 **Domain Layer** – Business logic, entities, and caching policies.  
📌 **Application Layer** – Queries, commands, and caching strategies.  
📌 **Infrastructure Layer** – Redis integration & repository patterns.  
📌 **Presentation Layer** – API controllers for cache interactions.  

---

## 🔄 How It Works  

### **1️⃣ Cache-Aside (Lazy Loading)**
🔹 Fetch from cache → If **miss**, load from DB → Store in cache → Return response.  

### **2️⃣ Write-Through**
🔹 Write to cache & DB **simultaneously** → Ensures **strong consistency**.  

### **3️⃣ Cache Invalidation**
🔹 Uses **Redis Pub/Sub** to remove outdated cache **when data updates**.  

---

## 🚀 Getting Started  

### **📌 Prerequisites**  
✅ [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)  
✅ [Redis](https://redis.io/download) (for distributed caching)  
✅ [Docker](https://www.docker.com/) (for running Redis container)  

### **Step 1: Clone the Repository**  
```bash
git clone https://github.com/MrEshboboyev/distributed-caching-system-with-cache-invalidation.git
cd distributed-caching-system-with-cache-invalidation
```

### **Step 2: Install Dependencies**  
```bash
dotnet restore
```

### **Step 3: Run Redis (Docker)**  
```bash
docker run -d -p 6379:6379 redis
```

### **Step 4: Run the Application**  
```bash
dotnet run --project src/App
```

---

## 🔧 Configuration  

Customize Redis settings in `appsettings.json`:  

```json
{
  "RedisSettings": {
    "ConnectionString": "localhost:6379",
    "InvalidationChannel": "cache-invalidation"
  },
  "CacheSettings": {
    "DefaultExpirationMinutes": 60
  }
}
```

---

## 🌐 API Endpoints  

| Method | Endpoint              | Description |
|--------|----------------------|-------------|
| **POST** | `/api/products`       | Create a product (Write-Through) |
| **GET**  | `/api/products/{id}`  | Fetch a product (Cache-Aside) |
| **DELETE** | `/api/products/{id}` | Delete a product (Invalidates Cache) |

---

## 🧪 Testing  

### **Unit Tests**  
Run tests to ensure system stability:  
```bash
dotnet test
```

### **Manual API Testing**  
📌 **Use Postman** or any REST client to:  
✅ **Create a product** → `/api/products`  
✅ **Fetch the product** → `/api/products/{id}` (data should be cached)  
✅ **Delete the product** → `/api/products/{id}` (cache invalidated)  

---

## 🎯 Why Use This Project?  

✅ **Lightning Fast Responses** – Eliminates unnecessary DB queries.  
✅ **Scalable & Fault-Tolerant** – Designed for **high-load applications**.  
✅ **Cache Consistency** – Implements **cache invalidation strategies**.  
✅ **Ready for Production** – Follows **best practices** in caching.  

---

## 📜 License  

This project is licensed under the **MIT License**. See [LICENSE](LICENSE) for details.  

---

## 📞 Contact  

For feedback, contributions, or questions:  
📧 **Email**: mreshboboyev@gmail.com
💻 **GitHub**: [MrEshboboyev](https://github.com/MrEshboboyev)  

---

🚀 **Supercharge your .NET apps with blazing-fast caching!** Clone the repo & start optimizing today!  
