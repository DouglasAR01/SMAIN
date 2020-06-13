CREATE  TABLE Usuario(
  cedula VARCHAR(10) NOT NULL,
  nombre_1 VARCHAR(30) NOT NULL,
  nombre_2 VARCHAR(30),
  apellido_1 VARCHAR(30) NOT NULL,
  apellido_2 VARCHAR(30),
  admin BOOLEAN NOT NULL,
  password VARCHAR(10) NOT NULL,
  token VARCHAR(250) NULL,
  PRIMARY KEY (cedula)
);
CREATE TABLE Cuenta(
  id SERIAL,
  balance DECIMAL NOT NULL,
  cedula VARCHAR(10) NOT NULL,
  PRIMARY KEY (id),
  FOREIGN KEY (cedula) REFERENCES Usuario(cedula)
);

-- INSERCIONES DE USUARIOS --

INSERT INTO Usuario(
  cedula,
  nombre_1,
  nombre_2,
  apellido_1,
  apellido_2,
  admin,
  password
) VALUES (
  "101",
  "Alice",
  "Ximena",
  "Jímenez",
  "Altozano",
  1,
  "prueba"
);
INSERT INTO Usuario(
  cedula,
  nombre_1,
  nombre_2,
  apellido_1,
  apellido_2,
  admin,
  password
) VALUES (
  "102",
  "Bob",
  "Alejandro",
  "Ramirez",
  "Toloza",
  0,
  "prueba"
);
INSERT INTO Usuario(
  cedula,
  nombre_1,
  nombre_2,
  apellido_1,
  apellido_2,
  admin,
  password
) VALUES (
  "103",
  "James",
  "Alexander",
  "Guevara",
  "Cifuentes",
  0,
  "prueba"
);

-- INSERCIONES DE BALANCES (EN EUROS) --
INSERT INTO Cuenta(balance, cedula) VALUES ("1000","101");
INSERT INTO Cuenta(balance, cedula) VALUES ("500","102");
INSERT INTO Cuenta(balance, cedula) VALUES ("700","103");
