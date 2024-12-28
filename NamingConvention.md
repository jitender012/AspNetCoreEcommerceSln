1. Foreign Key Naming
Format: FK_<ChildTable>_<ParentTable>

Example: FK_Products_Categories

Tables Updated:
Brand, Product, ProductImages, Category, Cart, CartItems, Feedback, FeedbackImages, Wishlist, Q&A, ProductDiscount

Tables Added:
CartItems, FeedbackImages, ProductDiscount

ProductItem
pk id
fk productId
SKU
qty
price

Product
pk id
fk categoryid
name
description

productCategory
pk id
fk parentCategoryId
categoryname

Variation
pk id
fk categoryId
name

variationOption
pk id
fk variationId
value

productConfiguration
fk productItemId
fk variationOptionId

________________
ProductItem:
Represents specific product variations like SKU, quantity, and price.

id	productId	SKU	qty	price
1	1	TSHIRT-RD-L	100	499
2	1	TSHIRT-BL-M	50	499
3	2	MUG-RED	200	199

Product:
Represents the main product (e.g., T-shirt, Mug).

id	categoryId	name	description
1	1	T-Shirt	Cotton T-shirt with logo
2	2	Mug	Ceramic coffee mug

ProductCategory:
Handles product categories (including parent-child hierarchy).

id	parentCategoryId	categoryName
1	NULL	Apparel
2	NULL	Accessories
3	1	T-Shirts
4	2	Mugs

ProductFeatures:
Represents types of variations, such as Color, Size, etc.

id	name
1	Color
2	Size

VariationCategory (Many-to-Many Relationship between Variation and Category):
Links variations to multiple categories.

id	ProductFeaturesId	categoryId
1	1					1
2	1					2
3	2					1
4	2					3

FeatureOption:
Represents specific options for each Feature (e.g., Red, Blue for Color).

id	FeatureId	value
1	1			Red
2	1			Blue
3	2			Small
4	2			Large

ProductConfiguration:
Links a ProductItem to its VariationOption. This allows specific product items to be configured with selected variation options (e.g., a T-shirt in Red and Large size).


productVarientId	FeatureOptionId
1					1
1					3
2					2
2					4
3					1



identity column data types

--int			
WishlistId
CartItemId
FeedbackId
FeedbackImageId
NotificationId
QueryId
PurchaseOrderId
InventoryId

---uniqueidentifier
CartId
OrderItemId
PurchaseOrderItemId