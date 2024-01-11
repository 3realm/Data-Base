# Задание 1
SELECT customer.first_name, customer.last_name, country.country
FROM customer
JOIN address ON customer.address_id = address.address_id
JOIN city ON address.city_id = city.city_id
JOIN country ON city.country_id = country.country_id
WHERE country.country IN ('Russian Federation', 'Angola');
# Задание 2 
SELECT film.title AS 'Название', category.name AS 'Жанр'
FROM film
JOIN film_actor ON film.film_id = film_actor.film_id
JOIN actor ON film_actor.actor_id = actor.actor_id
JOIN film_category ON film.film_id = film_category.film_id
JOIN category ON film_category.category_id = category.category_id
WHERE actor.actor_id = 3;
# Задание 3
SELECT category.name AS 'Жанр', SUM(payment.amount) AS 'Доход'
FROM film
JOIN film_category ON film.film_id = film_category.film_id
JOIN category ON film_category.category_id = category.category_id
JOIN inventory ON film.film_id = inventory.film_id
JOIN rental ON inventory.inventory_id = rental.inventory_id
JOIN payment ON rental.rental_id = payment.rental_id
WHERE MONTH(payment.payment_date) = 7
GROUP BY category.name
ORDER BY SUM(payment.amount) DESC
LIMIT 10;
# Задание 4 
SELECT customer.first_name, customer.last_name, COUNT(rental.rental_id) AS Количество_фильмов
FROM customer
INNER JOIN rental ON customer.customer_id = rental.customer_id
INNER JOIN inventory ON rental.inventory_id = inventory.inventory_id
INNER JOIN film_actor ON inventory.film_id = film_actor.film_id
INNER JOIN actor ON film_actor.actor_id = actor.actor_id
WHERE actor.actor_id = 2
GROUP BY customer.customer_id
ORDER BY COUNT(rental.rental_id) DESC
LIMIT 9, 5;
# Задагие 5
SELECT store.store_id, city.city, country.country, SUM(payment.amount) AS суммарный_доход
FROM store
INNER JOIN address ON store.address_id = address.address_id
INNER JOIN city ON address.city_id = city.city_id
INNER JOIN country ON city.country_id = country.country_id
INNER JOIN customer ON store.store_id = customer.store_id
INNER JOIN payment ON customer.customer_id = payment.customer_id
# первая неделя продаж
WHERE payment.payment_date BETWEEN 
    (SELECT MIN(payment_date) FROM payment) AND 
    DATE_ADD((SELECT MIN(payment_date) FROM payment), INTERVAL 7 DAY)
GROUP BY store.store_id;
# Задание 6
SELECT film.title AS фильм, actor.first_name AS имя_актера, actor.last_name AS фамилия_актера
FROM film
JOIN film_actor ON film.film_id = film_actor.film_id
JOIN actor ON film_actor.actor_id = actor.actor_id
WHERE film.film_id = (
    SELECT film_id
    FROM (
        SELECT film.film_id, SUM(payment.amount) AS доход
        FROM film
        JOIN inventory ON film.film_id = inventory.film_id
        JOIN rental ON inventory.inventory_id = rental.inventory_id
        JOIN payment ON rental.rental_id = payment.rental_id
        GROUP BY film.film_id
        ORDER BY доход DESC
        LIMIT 1
    ) AS top_film
);
# Задание 7
SELECT 
    c.customer_id AS customer_id,
    c.first_name AS customer_first_name,
    c.last_name AS customer_last_name,
    a.actor_id AS actor_id,
    a.first_name AS actor_first_name,
    a.last_name AS actor_last_name
FROM customer c
LEFT JOIN actor a ON c.last_name = a.last_name;
# Задание 8
SELECT
    actor.actor_id,
    actor.first_name AS actor_first_name,
    actor.last_name AS actor_last_name,
    customer.first_name AS customer_first_name,
    customer.last_name AS customer_last_name
FROM customer
RIGHT JOIN actor ON customer.last_name = actor.last_name;
# Задание 9
SELECT
    MAX(film.length) AS most_lenght,
    
    (SELECT COUNT(film.film_id)
    FROM film
    WHERE film.length = (SELECT MAX(film.length) FROM film)) AS count_of_most_lenght_films,    
    
    MIN(film.length) AS smallest_lenght,    
    
    (SELECT COUNT(film.film_id)
    FROM film
    WHERE film.length = (SELECT MIN(film.length) FROM film)) AS count_of_smallest_lenght_films,
    
    (SELECT MAX(count_actor)
		FROM (SELECT film.film_id,COUNT(film_actor.actor_id) AS count_actor
			FROM film
            INNER JOIN film_actor ON film.film_id = film_actor.film_id
            GROUP BY 1
            ORDER BY 2 DESC
            LIMIT 1) as actors) 
	AS  count_actor_max,

 (SELECT MIN(count_actor)
		FROM (SELECT film.film_id,COUNT(film_actor.actor_id) AS count_actor
			FROM film
            INNER JOIN film_actor ON film.film_id = film_actor.film_id
            GROUP BY 1
            ORDER BY 2 
            LIMIT 1) as actors)
	AS  count_actor_min,

(SELECT COUNT(film_id) 
FROM (SELECT film_id, COUNT(actor_id) AS actor_count 
FROM film_actor 
GROUP BY film_id) AS film_actors 
WHERE actor_count =  (SELECT MAX(count_actor)
		FROM (SELECT film.film_id,COUNT(film_actor.actor_id) AS count_actor
			FROM film
            INNER JOIN film_actor ON film.film_id = film_actor.film_id
            GROUP BY 1
            ORDER BY 2 DESC
            LIMIT 1) as actors))
AS films_with_count_actor_max,

(SELECT COUNT(film_id)
FROM (SELECT film_id, COUNT(actor_id) AS actor_count 
FROM film_actor 
GROUP BY film_id) AS film_actors 
WHERE actor_count = (SELECT MIN(count_actor)
		FROM (SELECT film.film_id,COUNT(film_actor.actor_id) AS count_actor
			FROM film
            INNER JOIN film_actor ON film.film_id = film_actor.film_id
            GROUP BY 1
            ORDER BY 2
            LIMIT 1) as actors)) AS films_with_count_actor_min

FROM film;