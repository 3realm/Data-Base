CREATE ALGORITHM = MERGE VIEW dish_amount
AS
SELECT *
FROM order_list
WHERE amount = 2;
# ORDER BY 
CREATE ALGORITHM = TEMPTABLE VIEW orders_date
AS
SELECT *
FROM order_client
WHERE date < "2022-11-11";
# OB
#
CREATE VIEW dish_price
AS
SELECT *
FROM dish
WHERE price BETWEEN 200 AND 500
WITH CHECK OPTION;

CREATE VIEW dish_price2
AS
SELECT *
FROM dish;

INSERT INTO dish_price (id_dish, name_dish, price, id_type) 
VALUES ('44', 'er', '111', '3');

INSERT INTO dish_price2 (id_dish, name_dish, price, id_type) 
VALUES ('44', 'er', '111', '3');