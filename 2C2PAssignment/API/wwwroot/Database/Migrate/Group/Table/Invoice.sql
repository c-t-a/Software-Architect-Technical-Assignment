CREATE TABLE Invoice (
	Transaction_ID TEXT NOT NULL,
	AMOUNT DECIMAL NOT NULL,
	Currency_Code TEXT NOT NULL,
	Transaction_Date TIMESTAMP(0) WITHOUT TIME ZONE NOT NULL,
	Status CHAR NOT NULL
);