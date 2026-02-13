# MongoDB Collections Structure

Bu loyiha MongoDB-da quyidagi kolleksiyalardan foydalanadi. GUID'lar standart binary formatda saqlanadi.

### 1. Organizations
```json
{
  "_id": "UUID",
  "Name": "Stock Pro",
  "Code": "SP001",
  "SubscriptionType": 3,
  "Currency": "USD",
  "Timezone": "UTC",
  "LowStockThreshold": 10,
  "IsActive": true,
  "CreatedAt": "ISODate",
  "DeletedAt": null
}
```

### 2. Products
```json
{
  "_id": "UUID",
  "OrganizationId": "UUID",
  "Sku": "PRD-001",
  "Name": "iPhone 15 Pro",
  "CategoryId": "UUID",
  "Unit": 0,
  "Cost": 900,
  "SellingPrice": 1100,
  "CreatedAt": "ISODate",
  "DeletedAt": null
}
```

### 3. Warehouses
```json
{
  "_id": "UUID",
  "OrganizationId": "UUID",
  "Name": "Main Warehouse",
  "Code": "MW-01",
  "CreatedAt": "ISODate"
}
```

### Indexes (Tavsiya etiladi)
- `Organizations`: `{ "Code": 1 }` (Unique)
- `Products`: `{ "OrganizationId": 1, "Sku": 1 }` (Unique)
- `Inventory`: `{ "OrganizationId": 1, "ProductId": 1, "WarehouseId": 1 }` (Unique)
