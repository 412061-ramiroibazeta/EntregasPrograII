CREATE DATABASE Facturas
GO
USE Facturas
GO 



CREATE TABLE Forma_pago (
    id_forma_pago INT NOT NULL,
    forma_pago VARCHAR(50) NOT NULL,
    CONSTRAINT PK_Forma_pago PRIMARY KEY (id_forma_pago)
);

CREATE TABLE Facturas (
    nro_factura INT NOT NULL IDENTITY(1,1),
    fecha DATE NOT NULL,
    id_forma_pago INT NOT NULL,
    cliente varchar(50) NOT NULL,
    CONSTRAINT PK_Facturas PRIMARY KEY (nro_factura),
    CONSTRAINT FK_Factura_FormaPago FOREIGN KEY (id_forma_pago) 
	REFERENCES Forma_pago(id_forma_pago)
);

CREATE TABLE Articulos (
    id_articulo INT NOT NULL,
    nombre VARCHAR(100) NOT NULL,
    precio_unitario DECIMAL(10, 2) NOT NULL,
    CONSTRAINT PK_Articulos PRIMARY KEY (id_articulo)
);

CREATE TABLE Detalle_factura (
    id_detalle_factura INT NOT NULL,
    id_factura INT NOT NULL,
    id_articulo INT NOT NULL,
    precio DECIMAL(10, 2) NOT NULL,
    cantidad INT NOT NULL,
    CONSTRAINT PK_Detalle_factura PRIMARY KEY (id_detalle_factura, id_factura),
    CONSTRAINT FK_Detalle_factura_Factura FOREIGN KEY (id_factura) 
	REFERENCES Facturas(nro_factura),
    CONSTRAINT FK_Detalle_factura_Articulo FOREIGN KEY (id_articulo) 
	REFERENCES Articulos(id_articulo)
);

SET DATEFORMAT DMY

---------------------------------------------------------------SP------------------------------------------------------------------------

------------------------------------------------------------  FACTURAS
-- Obtener todos
CREATE PROCEDURE sp_get_all_facturas
AS
BEGIN
    SELECT f.nro_factura, f.fecha, f.cliente, fp.id_forma_pago, fp.forma_pago FROM Facturas f, Forma_pago fp WHERE f.id_forma_pago = fp.id_forma_pago;;
END


CREATE PROCEDURE sp_get_factura
@nro_factura INT
AS
BEGIN
    SELECT f.nro_factura, f.fecha, f.cliente, fp.id_forma_pago, fp.forma_pago FROM Facturas f, Forma_pago fp WHERE f.id_forma_pago = fp.id_forma_pago;
END


-- Nueva factura
CREATE PROCEDURE sp_insertar_factura
	@nro_factura int output,
	-- fecha lo agrego como getdate()
	@id_forma_pago int,
	@cliente varchar(50)
AS
BEGIN
	INSERT INTO Facturas(fecha, id_forma_pago, cliente) VALUES (GETDATE(), @id_forma_pago, @cliente);
	SET @nro_factura = SCOPE_IDENTITY();
END

-- Nuevo registro detalle_factura
CREATE PROCEDURE sp_insert_detalle_factura
    @id_detalle_factura INT,
    @id_factura INT,
    @id_articulo INT,
    @precio DECIMAL(10, 2),
    @cantidad INT
AS
BEGIN
    INSERT INTO Detalle_factura (id_detalle_factura, id_factura, id_articulo, precio, cantidad)
    VALUES (@id_detalle_factura, @id_factura, @id_articulo, @precio, @cantidad);
END;



CREATE PROCEDURE sp_get_all_detalle_factura
AS
BEGIN
    SELECT * FROM Detalle_factura;
END;


CREATE PROCEDURE sp_get_detalle_nro_f
@nro_factura INT
AS
BEGIN
    SELECT a.id_articulo, df.precio, df.cantidad, a.nombre FROM Detalle_factura df, Articulos a WHERE id_factura = @nro_factura AND df.id_articulo = a.id_articulo;
END;

CREATE PROCEDURE sp_delete_detalle
@nro_factura int
AS
BEGIN
    DELETE FROM Detalle_factura WHERE id_factura = @nro_factura;
END;

CREATE PROCEDURE sp_edit_header_factu
	@nro_factura int,
	-- fecha lo agrego como getdate()
	@id_forma_pago int,
	@cliente varchar(50)
AS
BEGIN
    UPDATE Facturas
	SET id_forma_pago = @id_forma_pago, cliente = @cliente
	WHERE nro_factura = @nro_factura;
END;



CREATE PROCEDURE sp_consultar_facturas 
    @fecha DATE = NULL,
    @id_forma_pago INT = NULL
AS
BEGIN
    SELECT 
        f.nro_factura,
        f.fecha,
        f.id_forma_pago,
		fp.forma_pago,
        f.cliente
    FROM 
        Facturas f, Forma_pago fp
    WHERE
        (@fecha IS NULL OR Fecha = @fecha)
        AND
        (@id_forma_pago IS NULL OR fp.id_forma_pago = @id_forma_pago)
		AND f.id_forma_pago = fp.id_forma_pago;
END;



------------------------------------------------------------  ARTICULOS
-- Obtener todos
CREATE PROCEDURE sp_get_all_articulos
AS
BEGIN
    SELECT * FROM Articulos;
END;

-- Articulo ID
CREATE PROCEDURE sp_get_articulo_id
@id_articulo int
AS
BEGIN
    SELECT * FROM Articulos WHERE id_articulo = @id_articulo;
END;

-- Nuevo articulo
CREATE PROCEDURE sp_insert_articulos
    @id_articulo INT,
    @nombre VARCHAR(100),
    @precio_unitario DECIMAL(10, 2)
AS
BEGIN
    INSERT INTO Articulos (id_articulo, nombre, precio_unitario)
    VALUES (@id_articulo, @nombre, @precio_unitario);
END;

-- Borrar Articulo por ID
CREATE PROCEDURE sp_delete_articulo
@id_articulo int
AS
BEGIN
    DELETE FROM Articulos WHERE id_articulo = @id_articulo;
END;

CREATE PROCEDURE sp_update_articulos
    @id_articulo INT,
    @nombre VARCHAR(100),
    @precio_unitario DECIMAL(10, 2)
AS
BEGIN
    UPDATE Articulos
    SET id_articulo = @id_articulo, nombre = @nombre, precio_unitario = @precio_unitario
	WHERE id_articulo = @id_articulo
END;

  
--------------------------------------------------------------FORMA DE PAGO--
-- Obtener todos
CREATE PROCEDURE sp_get_all_forma_pago
AS
BEGIN
    SELECT * FROM Forma_pago;
END;

CREATE PROCEDURE sp_get_forma_pago
@id_forma_pago INT
AS
BEGIN
    SELECT * FROM Forma_pago WHERE id_forma_pago = @id_forma_pago;
END;

-- Nuevo registro
CREATE PROCEDURE sp_insert_forma_pago
    @id_forma_pago INT,
    @forma_pago VARCHAR(50)
AS
BEGIN
    INSERT INTO Forma_pago (id_forma_pago, forma_pago)
    VALUES (@id_forma_pago, @forma_pago);
END;

CREATE PROCEDURE sp_delete_tipo_pago
@id_forma_pago int
AS
BEGIN
    DELETE FROM Forma_pago WHERE id_forma_pago = @id_forma_pago;
END;

Insert into Articulos(id_articulo,nombre,precio_unitario)
Values (3, 'Auricular', 80)

