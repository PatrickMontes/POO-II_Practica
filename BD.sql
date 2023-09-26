-- Crear la base de datos "PracticaPOO"
create database PracticaPOO

-- Usar la base de datos "PracticaPOO"
use PracticaPOO

-- Eliminar la base de datos "PracticaPOO" 
drop database PracticaPOO


------------TABLAS------------
create table Producto(
	idProducto int primary key identity,
	nombreProducto varchar(50)
);

create table Proveedor(
	idProveedor int primary key identity,
	nombreProveedor varchar(100)
);

create table DetalleCompra(
	idDetalleCompra int primary key identity,
	idProducto int not null foreign key references Producto,
	idProveedor int not null foreign key references Proveedor,
	cantidad int,
	precio decimal(9,2),
	fechaCompra date,
	estadoCompra char(1)
);

------------VALORES------------
insert into Producto values ('Arroz (1kg)'), ('Leche (1L) '), ('Cafe Negro (500g)'), ('Aceite (1L)'), ('Queso (400g)'), ('Yogur (2L)')
insert into Proveedor values ('Alimentos S.A'), ('Laive'), ('Molino de Harina S.A'), ('Alicorp')
insert into DetalleCompra values(1, 1, 500, 4.50, '20230810', 'A'),
								(3, 3, 350, 7.00, '20230901', 'A'),
								(2, 2, 700, 10.50, '20230905', 'A'),
								(6, 2, 300, 15.70, '20230910', 'A'),
								(4, 4, 270, 8.50, '20230912', 'A'),
								(5, 4, 440, 6.70, '20230915', 'A')

------------STORE PROCEDURE------------

-- Procedimiento almacenado para listar productos
create procedure sp_ListaProducto
as
	select*from Producto

-- Procedimiento almacenado para listar proveedores
create procedure sp_ListaProveedor
as
	select*from Proveedor

-- Procedimiento almacenado para listar detalles de compra
--que tengan como estado de compra "A"
create procedure sp_ListaDetalleCompra
as
	set dateformat dmy
	select dc.idDetalleCompra, pd.nombreProducto, pd.idProducto, pv.nombreProveedor, pv.idProveedor, dc.cantidad, dc.precio, convert(char(10), dc.fechaCompra, 103) as fechaCompra
	from Producto pd inner join DetalleCompra dc on pd.idProducto = dc.idProducto
		 inner join Proveedor pv on dc.idProveedor = pv.idProveedor
	where dc.estadoCompra = 'A'

-- Procedimiento almacenado para agregar un nuevo detalle de compra
create procedure sp_NuevoDetalleCompra
@idProducto int,
@idProveedor int,
@cantidad int,
@precio decimal(9, 2),
@fechaCompra varchar(10),
@estadoCompra char(1)
as
	set dateformat dmy
	insert into DetalleCompra values(@idProducto, @idProveedor, @cantidad, @precio, convert(date, @fechaCompra), @estadoCompra)

-- Procedimiento almacenado para modificar un detalle de compra existente
create procedure sp_ModificarDetalleCompra
@idDetalleCompra int,
@idProducto int,
@idProveedor int,
@cantidad int,
@precio decimal(9, 2),
@fechaCompra varchar(10),
@estadoCompra char(1)
as
	set dateformat dmy
	update DetalleCompra set idProducto = @idProducto, idProveedor = @idProveedor, cantidad = @cantidad, 
							 precio = @precio, fechaCompra = convert(date, @fechaCompra), estadoCompra = @estadoCompra
	where idDetalleCompra = @idDetalleCompra

-- Procedimiento almacenado para marcar un detalle de compra como eliminado
--colocando su estado de compra en "E"
create procedure sp_EliminarDetalleCompra
@idDetalleCompra int
as
	update DetalleCompra
	set estadoCompra = 'E'
	where idDetalleCompra = @idDetalleCompra
