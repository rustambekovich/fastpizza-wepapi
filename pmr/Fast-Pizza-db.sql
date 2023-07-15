


create table categories
(
	id bigint generated always as identity primary key,
	name varchar(200) not null,
	description text,
	image_path text not null,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table products
(
	id bigint generated always as identity primary key,
	name varchar(50) not null,
	description text,
	image_path text not null,
	unit_price double PRECISION not null,
	category_id bigint references categories (id),
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

CREATE TABLE customers
(
    id bigint generated always as identity primary key,
    full_name character varying(200),
    phone_number character varying(13),
    image_path_customer text,
    email character varying(200) NOT NULL,
    created_at timestamp without time zone default now(),
    updated_at timestamp without time zone default now()
);

CREATE TABLE users
(
	id bigint generated always as identity primary key,
    first_name character varying(50) NOT NULL,
    last_name character varying(50) NOT NULL,
    middle_name character varying(50),
    phone_number character varying(13) NOT NULL,
    passport_seria_number character varying(9),
    is_male boolean NOT NULL,
    birth_date date NOT NULL,
    was_born text NOT NULL,
    password_hash text NOT NULL,
    salt text NOT NULL,
    image_path text NOT NULL,
    identity_role text NOT NULL,
    created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);


CREATE TABLE deliveries
(
    id bigint generated always as identity primary key,
    first_name character varying(50) NOT NULL,
    last_name character varying(50) NOT NULL,
    middle_name character varying(50),
    phone_number character varying(13) NOT NULL,
    passport_seria_number character varying(9),
    is_male boolean NOT NULL,
    birth_date date NOT NULL,
    was_born text NOT NULL,
    password_hash text NOT NULL,
    salt text NOT NULL,
    image_path text NOT NULL,
    created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);
CREATE TABLE branch
(
    id bigint generated always as identity primary key,
    name character varying(200) NOT NULL,
    latitude double precision NOT NULL,
    longitude double precision NOT NULL,
    created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);
CREATE TABLE orders
(
    id bigint generated always as identity primary key,
    customer_id bigint references customers(id),
    delivery_id bigint references deliveries(id),
    status text NOT NULL,
    products_price double precision NOT NULL,
    delivery_price double precision,
    result_price double precision NOT NULL,
    latitude double precision,
    longitude double precision,
    payment_type text,
    is_paid boolean ,
    description text,
    order_type text ,
    branch_id bigint references branch(id),
    created_at timestamp without time zone default now(),
    updated_at timestamp without time zone default now()
);



CREATE TABLE order_items
(
    id bigint generated always as identity primary key,
    order_id bigint references orders(id),
    product_id bigint references products(id),
   	quantity  bigint NOT NULL,
    total_price  double precision NOT NULL,
    discount_price  double precision NOT NULL,
    result_price double precision NOT NULL,
    created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

