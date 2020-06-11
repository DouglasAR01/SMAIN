CREATE  TABLE Usuario(
  usr_cedula VARCHAR(10) NOT NULL,
  usr_nombre_1 VARCHAR(30) NOT NULL,
  usr_nombre_2 VARCHAR(30),
  usr_apellido_1 VARCHAR(30) NOT NULL,
  usr_apellido_2 VARCHAR(30),
  usr_admin BOOLEAN NOT NULL,
  PRIMARY KEY (usr_cedula)
);
CREATE TABLE Cuenta(
  cta_id SERIAL,
  cta_balance DECIMAL NOT NULL,
  fk_usr_cedula VARCHAR(10) NOT NULL,
  PRIMARY KEY (cta_id),
  FOREIGN KEY (fk_usr_cedula) REFERENCES Usuario(usr_cedula)
);

-- INSERCIONES DE USUARIOS --

INSERT INTO Usuario(
  usr_cedula,
  usr_nombre_1,
  usr_nombre_2,
  usr_apellido_1,
  usr_apellido_2,
  usr_admin
) VALUES (
  "101",
  "Alice",
  "Ximena",
  "Jímenez",
  "Altozano",
  1
);
INSERT INTO Usuario(
  usr_cedula,
  usr_nombre_1,
  usr_nombre_2,
  usr_apellido_1,
  usr_apellido_2,
  usr_admin
) VALUES (
  "102",
  "Bob",
  "Alejandro",
  "Ramirez",
  "Toloza",
  0
);
INSERT INTO Usuario(
  usr_cedula,
  usr_nombre_1,
  usr_nombre_2,
  usr_apellido_1,
  usr_apellido_2,
  usr_admin
) VALUES (
  "103",
  "James",
  "Alexander",
  "Guevara",
  "Cifuentes",
  0
);

-- INSERCIONES DE BALANCES (EN EUROS) --

INSERT INTO Cuenta(cta_balance, fk_usr_cedula) VALUES ("1000","101");
INSERT INTO Cuenta(cta_balance, fk_usr_cedula) VALUES ("500","102");
INSERT INTO Cuenta(cta_balance, fk_usr_cedula) VALUES ("700","103");
