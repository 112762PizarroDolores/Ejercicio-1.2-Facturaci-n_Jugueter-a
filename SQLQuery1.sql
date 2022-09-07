 

--BASE DE DATOS JUGUETERIA PARA LA TERCERA ENTREGA


--CREATE DATABASE FACTUACION_JUGUETERIA_112762
USE FACTUACION_JUGUETERIA_112762


CREATE TABLE T_FORMAS_PAGO
	(
	ID_FORMA_PAGO INT,
	NOMBRE VARCHAR (50)
	CONSTRAINT PK_FORMAS_PAGO PRIMARY KEY (ID_FORMA_PAGO)
	)

CREATE TABLE T_ARTICULOS
    (
    ID_ARTICULO INT IDENTITY(1,1) NOT NULL,
    N_ARTICULO VARCHAR (255) NOT NULL,
	PRECIO NUMERIC (8,2) NOT NULL
	CONSTRAINT PK_ARTICULOS PRIMARY KEY (ID_ARTICULO)
    )
	
CREATE TABLE T_FACTURAS
    (
	FACTURA_NRO INT IDENTITY(1,1) NOT NULL,
	FECHA DATE NOT NULL,
	CLIENTE VARCHAR (255) NOT NULL,
	DESCUENTO NUMERIC (5, 2) NULL,
	ID_FORMA_PAGO INT NOT NULL,
	TOTAL NUMERIC (8, 2) NOT NULL,
	CONSTRAINT PK_T_FACTURAS PRIMARY KEY (FACTURA_NRO),
	CONSTRAINT FK_T_FACTURAS_T_FORMAS_PAGO FOREIGN KEY (ID_FORMA_PAGO)
	REFERENCES T_FORMAS_PAGO (ID_FORMA_PAGO)
    )

CREATE TABLE T_DETALLES_FACTURA
    (
	FACTURA_NRO INT NOT NULL,
	DETALLE_NRO INT NOT NULL,
	ID_ARTICULO INT NOT NULL,
	CANTIDAD INT NOT NULL,
	CONSTRAINT PK_T_DETALLES_FACTURA PRIMARY KEY (FACTURA_NRO, DETALLE_NRO),
	CONSTRAINT FK_T_DETALLES_FACTURA_T_FACTURAS FOREIGN KEY (FACTURA_NRO)
	REFERENCES T_FACTURAS (FACTURA_NRO),
	CONSTRAINT FK_T_DETALLES_FACTURA_T_ARTICULOS FOREIGN KEY (ID_ARTICULO)
	REFERENCES T_ARTICULOS (ID_ARTICULO)
	)
	
--GO
--CREATE PROCEDURE SP_REGISTRAR_BAJA_FACTURAS
	--@FACTURA_NRO INT
--AS
--BEGIN
	--UPDATE T_PRESUPUESTOS SET fecha_baja = GETDATE()
	--WHERE presupuesto_nro = @presupuesto_nro;
	
--END
GO

CREATE PROCEDURE SP_PROXIMO_ID
@NEXT INT OUTPUT
AS
BEGIN
	SET @NEXT = (SELECT MAX(ID_ARTICULO)+1  FROM T_ARTICULOS);
END

GO

CREATE PROCEDURE SP_INSERTAR_MAESTRO
	@CLIENTE VARCHAR(255), 
	@DESCUENTO NUMERIC(5,2),
	@TOTAL NUMERIC(8,2),
	@ID_FORMA_PAGO INT,
	@FACTURA_NRO INT OUTPUT
AS
BEGIN
	INSERT INTO T_FACTURAS(FECHA, CLIENTE, DESCUENTO, TOTAL, ID_FORMA_PAGO)
    VALUES (GETDATE(), @CLIENTE, @DESCUENTO, @TOTAL, @ID_FORMA_PAGO);
    --Asignamos el valor del �ltimo ID autogenerado (obtenido --  
    --mediante la funci�n SCOPE_IDENTITY() de SQLServer)	
    SET @FACTURA_NRO = SCOPE_IDENTITY();

END

GO

CREATE PROCEDURE SP_CONSULTAR_ARTICULOS
AS
BEGIN
	
	SELECT * FROM T_ARTICULOS;
END

GO

--CREATE PROCEDURE SP_CONSULTAR_FACTURAS 
	--@fecha_desde Date,
	--@fecha_hasta Date,
	--@cliente varchar(255),
	--@datos_baja varchar(1)
--AS
--BEGIN
	--SELECT * FROM T_FACTURAS
	--WHERE 
	 --((@fecha_desde is null and @fecha_hasta is  null) OR (fecha between @fecha_desde and @fecha_hasta))
	 --AND(@cliente is null OR (cliente like '%' + @cliente + '%'))
	 --AND (@datos_baja is null OR (@datos_baja = 'S') OR (@datos_baja = 'N' and fecha_baja IS  NULL))
	 --END
GO

--CREATE PROCEDURE SP_CONSULTAR_FACTURA_POR_ID
--	@nro int	
--AS
--BEGIN
	--SELECT *
	--FROM T_PRESUPUESTOS t, T_DETALLES_PRESUPUESTO t1, T_PRODUCTOS t2
	--WHERE t.presupuesto_nro = t1.presupuesto_nro
	--AND t1.id_producto = t2.id_producto
	--AND t.presupuesto_nro = @nro;
--END
GO

CREATE PROCEDURE SP_INSERTAR_DETALLE 
	@FACTURA_NRO INT,
	@DETALLE INT, 
	@ID_ARTICULO int, 
	@CANTIDAD INT
AS
BEGIN
	INSERT INTO T_DETALLES_FACTURA( FACTURA_NRO, DETALLE_NRO, ID_ARTICULO, CANTIDAD)
    VALUES (@FACTURA_NRO, @DETALLE, @ID_ARTICULO, @CANTIDAD);
  
END
GO

--CREATE PROCEDURE [dbo].[SP_REPORTE_ARTICULOS]
--AS
--BEGIN
	--SELECT t2.n_producto as producto, SUM(t1.cantidad) as cantidad
	--FROM T_FACTURAS t, T_DETALLES_FACTURA t1, T_ARTICULOS t2
	--WHERE t.FACTURA_NRO = t1.FACTURA_NRO
	--AND t1.id_producto = t2.id_producto
	--GROUP BY t2.N_ARTICULO 
--END

GO

CREATE PROCEDURE SP_CONSULTAR_FORMAS_PAGO
AS
BEGIN
	
	SELECT * FROM T_FORMAS_PAGO;
END

INSERT INTO T_FORMAS_PAGO (ID_FORMA_PAGO, NOMBRE) VALUES (1, 'CONTADO')
 INSERT INTO T_FORMAS_PAGO (ID_FORMA_PAGO, NOMBRE) VALUES (2,'CREDITO')
 INSERT INTO T_FORMAS_PAGO (ID_FORMA_PAGO, NOMBRE) VALUES (3, 'TRANSFERENCIA')
 INSERT INTO T_FORMAS_PAGO (ID_FORMA_PAGO, NOMBRE) VALUES (4,'DEBITO')

 INSERT INTO T_ARTICULOS( N_ARTICULO, PRECIO) VALUES ( 'BARBIE', 20000)
 INSERT INTO T_ARTICULOS( N_ARTICULO,PRECIO) VALUES ('BEBOTE', 5000)
 INSERT INTO T_ARTICULOS(N_ARTICULO, PRECIO) VALUES ('AUTITO', 3000)
 INSERT INTO T_ARTICULOS( N_ARTICULO,PRECIO) VALUES ('ROMPECABEZAS', 1000)
 INSERT INTO T_ARTICULOS( N_ARTICULO, PRECIO) VALUES ( 'GENERALA', 700)
 INSERT INTO T_ARTICULOS( N_ARTICULO,PRECIO) VALUES ('PELOTA', 2500)
