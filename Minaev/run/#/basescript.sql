SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

DROP ROLE IF EXISTS simbirgoadmin;
CREATE ROLE simbirgoadmin LOGIN SUPERUSER PASSWORD 'simbirgoadminpass';

ALTER DATABASE simbirgo OWNER TO postgres;

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

ALTER SCHEMA public OWNER TO pg_database_owner;

CREATE TABLE public."Accounts" (
    id bigint NOT NULL,
    username character varying NOT NULL,
    password character varying NOT NULL,
    isadmin boolean NOT NULL,
    balance double precision NOT NULL,
    createddatetimeutc timestamp without time zone NOT NULL,
    modifieddatetimeutc timestamp without time zone,
    isremoved boolean NOT NULL,
    createduserid bigint NOT NULL,
    modifieduserid bigint
);

ALTER TABLE public."Accounts" OWNER TO postgres;

CREATE TABLE public."BannedJwtTokens" (
    token character varying NOT NULL
);

ALTER TABLE public."BannedJwtTokens" OWNER TO postgres;

CREATE TABLE public."Rents" (
    id bigint NOT NULL,
    transportid bigint NOT NULL,
    userid bigint NOT NULL,
    timestartutc timestamp without time zone NOT NULL,
    timeendutc timestamp without time zone,
    priceofunit double precision NOT NULL,
    pricetype character varying NOT NULL,
    finalprice double precision,
    createddatetimeutc timestamp without time zone NOT NULL,
    createduserid bigint NOT NULL,
    modifieddatetimeutc timestamp without time zone,
    isremoved boolean NOT NULL
);


ALTER TABLE public."Rents" OWNER TO postgres;

CREATE TABLE public."Transports" (
    id bigint NOT NULL,
    identifier character varying NOT NULL,
    description character varying,
    transporttype character varying NOT NULL,
    model character varying NOT NULL,
    color character varying NOT NULL,
    ownerid bigint NOT NULL,
    latitude double precision NOT NULL,
    longitude double precision NOT NULL,
    canberented boolean NOT NULL,
    minuteprice double precision,
    dayprice double precision,
    createddatetimeutc timestamp without time zone NOT NULL,
    modifieddatetimeutc timestamp without time zone,
    isremoved boolean NOT NULL
);

ALTER TABLE public."Transports" OWNER TO postgres;

ALTER TABLE ONLY public."Accounts"
    ADD CONSTRAINT "Accounts_pkey" PRIMARY KEY (id);

ALTER TABLE ONLY public."BannedJwtTokens"
    ADD CONSTRAINT "BannedJwtTokens_pkey" PRIMARY KEY (token);

ALTER TABLE ONLY public."Rents"
    ADD CONSTRAINT "Rents_pkey" PRIMARY KEY (id);

