-- Table: public.member

-- DROP TABLE public.member;

CREATE TABLE public.member
(
    m_no text COLLATE pg_catalog."default" NOT NULL,
    "memberValueObject_userName" text COLLATE pg_catalog."default",
    "memberValueObject_password" text COLLATE pg_catalog."default",
    "memberValueObject_saltPassword" text COLLATE pg_catalog."default",
    "amountValueObject_monthlyIncome" text COLLATE pg_catalog."default",
    "amountValueObject_savings" text COLLATE pg_catalog."default",
    "amountValueObject_fixedCost" text COLLATE pg_catalog."default",
    "amountLimitValueObject__amountLimit" integer,
    utime timestamp without time zone NOT NULL,
    CONSTRAINT "PK_member" PRIMARY KEY (m_no)
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

ALTER TABLE public.member
    OWNER to postgres;