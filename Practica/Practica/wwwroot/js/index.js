////creación de un modelo
const _modeloCompra = {
    idDetalleCompra: 0,
    idProducto: 0,
    idProveedor: 0,
    cantidad: 0,
    precio: 0,
    fechaCompra: "",
}

//Función para mostrar la lista de personal
function MostrarCompra() {
    fetch("/Home/listaDetalleCompras")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        })

        .then(responseJson => {
            if (responseJson.length > 0) {
                $("#tablaCompra tbody").html("");
                responseJson.forEach((compra) => {
                    $("#tablaCompra tbody").append(
                        $("<tr>").append(
                            $("<td>").text(compra.refProducto.nombreProducto),
                            $("<td>").text(compra.refProveedor.nombreProveedor),
                            $("<td>").text(compra.cantidad),
                            $("<td>").text(compra.precio),
                            $("<td>").text(compra.fechaCompra),
                            $("<td>").append(
                                $("<button>").addClass("btn btn-primary boton-editar-compra")
                                    .text("Editar").data("dataCompra", compra),
                                $("<button>").addClass("btn btn-danger ms-2 boton-eliminar-compra")
                                    .text("Eliminar").data("dataCompra", compra),
                            )
                        )
                    )
                })
            }
        })
}
//entrando al evento cuando toda la página ya ha sido cargada ejecute algunas acciones
document.addEventListener("DOMContentLoaded", function () {
    MostrarCompra();

    fetch("/Home/listaProductos")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        })
        .then(responseJson => {
            if (responseJson.length > 0) {
                responseJson.forEach((item) => {
                    $("#cboProducto").append(
                        $("<option>").val(item.idProducto).text(item.nombreProducto)
                    )
                })
            }
        })

    fetch("/Home/listaProveedores")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        })
        .then(responseJson => {
            if (responseJson.length > 0) {
                responseJson.forEach((item) => {
                    $("#cboProveedor").append(
                        $("<option>").val(item.idProveedor).text(item.nombreProveedor)
                    )
                })
            }
        })

    $("#txtFechaCompra").datepicker({
        format: "dd/mm/yyyy",
        autoclose: true,
        todayHighlight: true,
        language: "es"
    });
}, false);



function MostrarModal() {
    $("#cboProducto").val(_modeloCompra.idProducto == 0 ? $("#cboProducto option:first").val() : _modeloCompra.idProducto);
    $("#cboProveedor").val(_modeloCompra.idProveedor == 0 ? $("#cboProveedor option:first").val() : _modeloCompra.idProveedor);
    $("#txtCantidad").val(_modeloCompra.cantidad);
    $("#txtPrecio").val(_modeloCompra.precio);
    $("#txtFechaCompra").val(_modeloCompra.fechaCompra);

    $("#modalCompra").modal("show");
}

$(document).on("click", ".boton-crear-compra", function () {
    _modeloCompra.idDetalleCompra = 0;
    _modeloCompra.idProducto = 0;
    _modeloCompra.idProveedor = 0;
    _modeloCompra.cantidad = 0;
    _modeloCompra.precio = 0;
    _modeloCompra.fechaCompra = "";

    MostrarModal();
})


$(document).on("click", ".boton-editar-compra", function () {
    const _compra = $(this).data("dataCompra");

    _modeloCompra.idDetalleCompra = _compra.idDetalleCompra;
    _modeloCompra.idProducto = _compra.refProducto.idProducto;
    _modeloCompra.idProveedor = _compra.refProveedor.idProveedor;
    _modeloCompra.cantidad = _compra.cantidad;
    _modeloCompra.precio = _compra.precio;
    _modeloCompra.fechaCompra = _compra.fechaCompra;

    // Rellenar los campos del modal con los datos de _modeloCompra
    MostrarModal();
});

$(document).on("click", ".boton-guardar-cambios-compras", function () {
    const modelo = {
        idDetalleCompra: _modeloCompra.idDetalleCompra,
        refProducto: {
            idProducto: $("#cboProducto").val()
        },
        refProveedor: {
            idProveedor: $("#cboProveedor").val()
        },
        cantidad: $("#txtCantidad").val(),
        precio: $("#txtPrecio").val(),
        fechaCompra: $("#txtFechaCompra").val()
    }

    if (_modeloPersonal.idPersonal == 0) {
        fetch("/Home/crearDetalleCompra", {
            method: "POST",
            headers: { "Content-Type": "application/json;charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {
                if (responseJson.valor) {
                    $("#modalCompra").modal("hide");
                    Swal.fire("Listo!", "Compra fue creado", "success");
                    MostrarCompra();
                }
                else
                    Swal.fire("Lo sentimos!", "No se pudo crear Compra", "error");
            })
    }
    else {
        fetch("/Home/editarDetalleCompra", {
            method: "PUT",
            headers: { "Content-Type": "application/json;charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {
                if (responseJson.valor) {
                    $("#modalCompra").modal("hide");
                    Swal.fire("Listo!", "Compra fue actualizado", "success");
                    MostrarCompra();
                }
                else
                    Swal.fire("Lo sentimos!", "No se pudo actualizar Compra", "error");
            })
    }
})