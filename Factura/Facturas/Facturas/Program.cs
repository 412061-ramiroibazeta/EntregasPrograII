using Facturas.Datos.Implementations;
using Facturas.Dominio;
using Facturas.Servicios;

FormaPago forma1 = new FormaPago { IdFormaPago = 1, FormaDePago = "Efectivo" };
FormaPago forma2 = new FormaPago { IdFormaPago = 2, FormaDePago = "Tarjeta de Crédito" };
FormaPago forma3 = new FormaPago { IdFormaPago = 3, FormaDePago = "Transferencia Bancaria" };

var fpRepo = new FormaPagoServices();

fpRepo.Save(forma1);
fpRepo.Save(forma2);    
fpRepo.Save(forma3);

Console.WriteLine("-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|");

Articulo articulo1 = new Articulo { IdArticulo = 1, Nombre = "Notebook", PrecioUnitario = 1000 };
Articulo articulo2 = new Articulo { IdArticulo = 2, Nombre = "Mouse", PrecioUnitario = 50 };

var articuloService = new ArticuloServices();

articuloService.Save(articulo1);
articuloService.Save(articulo2);

DetalleFactura detalle1 = new DetalleFactura
{
    Articulo = articulo1,
    Precio = 1000,
    Cantidad = 1
};

DetalleFactura detalle2 = new DetalleFactura
{
    Articulo = articulo2,
    Precio = 50,
    Cantidad = 2
};

DetalleFactura detalle3 = new DetalleFactura
{
    Articulo = articulo1,
    Precio = 1000,
    Cantidad = 1
};

Factura factura = new Factura
{
    NroFactura = 1,
    FormaPago = forma1,
    Cliente = "Ramiro"
};

factura.AniadirDetalle(detalle1);
factura.AniadirDetalle(detalle2);
factura.AniadirDetalle(detalle3); //pruebo si el verificador de articulos x2 en detalle duplicados funciona


Console.WriteLine($"Cliente: {factura.Cliente}, Forma de Pago: {factura.FormaPago.FormaDePago}");
Console.WriteLine("Detalles de la factura antes de guardar:");
foreach (var detalle in factura.Detalles)
{
    Console.WriteLine($"Artículo: {detalle.Articulo.Nombre}, Cantidad: {detalle.Cantidad}, Precio: {detalle.Precio}, Subtotal: {detalle.Subtotal()}");
}


var repo = new FacturaServices();
bool resultado = repo.Save(factura);

Console.WriteLine("Factura guardada exitosamente: " + resultado);

Console.WriteLine("-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|");

Factura factu = repo.GetById(1); // pongo 1 entendiendo que la base es identity y sé que es la primer factura

Console.WriteLine("Numero de factura: " + factu.ToString());
foreach (DetalleFactura df in factu.Detalles)
{
    Console.WriteLine("Articulo: "+ df.Articulo.Nombre +  ", Cantidad: " + df.Cantidad);
}

Console.WriteLine("-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|-|");

DetalleFactura detalle4 = new DetalleFactura
{
    Articulo = articulo1,
    Precio = 1000,
    Cantidad = 5
};

factura.AniadirDetalle(detalle4);
factura.Cliente = "Gonzalo";
factura.FormaPago = forma2;

Console.WriteLine($"Cliente: {factura.Cliente}, Forma de Pago: {factura.FormaPago.FormaDePago}");
Console.WriteLine("Detalles de la factura antes de guardar por segunda vez:");
foreach (var detalle in factura.Detalles)
{
    Console.WriteLine($"Artículo: {detalle.Articulo.Nombre}, Cantidad: {detalle.Cantidad}, Precio: {detalle.Precio}, Subtotal: {detalle.Subtotal()}");
}

bool prueba = repo.Edit(factura);

Console.WriteLine("Factura editada exitosamente: " + resultado);
