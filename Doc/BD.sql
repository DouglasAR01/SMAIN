CREATE  TABLE Usuario(
  id SERIAL,
  cedula VARCHAR(10) NOT NULL UNIQUE,
  nombre_1 VARCHAR(30) NOT NULL,
  nombre_2 VARCHAR(30),
  apellido_1 VARCHAR(30) NOT NULL,
  apellido_2 VARCHAR(30),
  role VARCHAR(1) NOT NULL DEFAULT 'u',
  password VARCHAR(10) NOT NULL,
  token VARCHAR(250) NULL,
  PRIMARY KEY (id)
);
CREATE TABLE Cuenta(
  id SERIAL,
  balance numeric(65,30) NOT NULL,
  id_usuario BIGINT UNSIGNED,
  PRIMARY KEY (id),
  FOREIGN KEY (id_usuario) REFERENCES Usuario(id)
);

-- INSERCIONES DE USUARIOS --

INSERT INTO Usuario(
  cedula,
  nombre_1,
  nombre_2,
  apellido_1,
  apellido_2,
  role,
  password
) VALUES (
  "101",
  "Alice",
  "Ximena",
  "Jímenez",
  "Altozano",
  "a",
  "prueba"
);
INSERT INTO Usuario(
  cedula,
  nombre_1,
  nombre_2,
  apellido_1,
  apellido_2,
  role,
  password
) VALUES (
  "102",
  "Bob",
  "Alejandro",
  "Ramirez",
  "Toloza",
  "u",
  "prueba"
);
INSERT INTO Usuario(
  cedula,
  nombre_1,
  nombre_2,
  apellido_1,
  apellido_2,
  role,
  password
) VALUES (
  "103",
  "James",
  "Alexander",
  "Guevara",
  "Cifuentes",
  "u",
  "prueba"
);

-- INSERCIONES DE BALANCES (EN EUROS) --
INSERT INTO Cuenta(balance, id_usuario) VALUES ("1000",1);
INSERT INTO Cuenta(balance, id_usuario) VALUES ("500",2);
INSERT INTO Cuenta(balance, id_usuario) VALUES ("700",3);
