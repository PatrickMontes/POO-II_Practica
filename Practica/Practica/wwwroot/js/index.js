// Modelo de compra para inicializar y mantener los datos del formulario
const _modeloCompra = {
    idDetalleCompra: 0,
    idProducto: 0,
    idProveedor: 0,
    cantidad: 0,
    precio: 0,
    fechaCompra: "",
}


// Función para mostrar la lista de compras
function MostrarCompra() {
    fetch("/Home/listaDetalleCompras")     // Realiza una solicitud GET para obtener la lista de compras
        .then(response => {
            // Verifica si la respuesta es exitosa
            return response.ok ? response.json() : Promise.reject(response)
        })

        .then(responseJson => {
            // Si hay compras en la respuesta, muestra los datos en la tabla
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


// Evento que se ejecuta cuando se carga el DOM
document.addEventListener("DOMContentLoaded", function () {
    // Llama a la función para mostrar las compras
    MostrarCompra();

    // Realiza una solicitud GET para obtener la lista de productos
    fetch("/Home/listaProductos")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        })
        .then(responseJson => {
            // Si hay productos en la respuesta, agrega opciones al select
            if (responseJson.length > 0) {
                responseJson.forEach((item) => {
                    $("#cboProducto").append(
                        $("<option>").val(item.idProducto).text(item.nombreProducto)
                    )
                })
            }
        })

    // Realiza una solicitud GET para obtener la lista de proveedores
    fetch("/Home/listaProveedores")
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        })
        .then(responseJson => {
            // Si hay proveedores en la respuesta, agrega opciones al select
            if (responseJson.length > 0) {
                responseJson.forEach((item) => {
                    $("#cboProveedor").append(
                        $("<option>").val(item.idProveedor).text(item.nombreProveedor)
                    )
                })
            }
        })


    // Configuración del datepicker
    $("#txtFechaCompra").datepicker({
        format: "dd/mm/yyyy",
        autoclose: true,
        todayHighlight: true,
        language: "es"
    });
}, false);


// Función para mostrar el modal de edición/creación de compras
function MostrarModal() {
    // Establece los valores en el modal
    $("#cboProducto").val(_modeloCompra.idProducto == 0 ? $("#cboProducto option:first").val() : _modeloCompra.idProducto);
    $("#cboProveedor").val(_modeloCompra.idProveedor == 0 ? $("#cboProveedor option:first").val() : _modeloCompra.idProveedor);
    $("#txtCantidad").val(_modeloCompra.cantidad);
    $("#txtPrecio").val(_modeloCompra.precio);
    $("#txtFechaCompra").val(_modeloCompra.fechaCompra);

    $("#modalCompra").modal("show");
}


// Evento click para crear una nueva compra
$(document).on("click", ".boton-crear-compra", function () {
    // Limpia el modelo de compra y muestra el modal
    _modeloCompra.idDetalleCompra = 0;
    _modeloCompra.idProducto = 0;
    _modeloCompra.idProveedor = 0;
    _modeloCompra.cantidad = 0;
    _modeloCompra.precio = 0;
    _modeloCompra.fechaCompra = "";

    MostrarModal();
})


// Evento click para editar una compra existente
$(document).on("click", ".boton-editar-compra", function () {
    // Obtiene los datos de la compra y muestra el modal
    const _compra = $(this).data("dataCompra");

    _modeloCompra.idDetalleCompra = _compra.idDetalleCompra;
    _modeloCompra.idProducto = _compra.refProducto.idProducto;
    _modeloCompra.idProveedor = _compra.refProveedor.idProveedor;
    _modeloCompra.cantidad = _compra.cantidad;
    _modeloCompra.precio = _compra.precio;
    _modeloCompra.fechaCompra = _compra.fechaCompra;

    MostrarModal();
});


// Evento click para guardar cambios en la compra
$(document).on("click", ".boton-guardar-cambios-compras", function () {
    // Obtiene los valores del formulario
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


    // Determina si se crea o edita una compra
    if (_modeloCompra.idDetalleCompra == 0) {
        // Realiza una solicitud POST para crear una compra
        fetch("/Home/crearDetalleCompra", {
            method: "POST",
            headers: { "Content-Type": "application/json;charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {
                // Muestra un mensaje de éxito o error
                if (responseJson.valor) {
                    $("#modalCompra").modal("hide");
                    Swal.fire("Creado!", "Detalle-Compra fue creado", "success");
                    MostrarCompra();
                }
                else
                    Swal.fire("Error!", "No se pudo crear Detalle-Compra", "error");
            })
    }
    else {
        // Realiza una solicitud PUT para editar una compra existente
        fetch("/Home/editarDetalleCompra", {
            method: "PUT",
            headers: { "Content-Type": "application/json;charset=utf-8" },
            body: JSON.stringify(modelo)
        })
            .then(response => {
                return response.ok ? response.json() : Promise.reject(response)
            })
            .then(responseJson => {
                // Muestra un mensaje de éxito o error
                if (responseJson.valor) {
                    $("#modalCompra").modal("hide");
                    Swal.fire("Actualizado!", "Detalle-Compra fue actualizado", "success");
                    MostrarCompra();
                }
                else
                    Swal.fire("Error!", "No se pudo actualizar el Detalle-Compra", "error");
            })
    }
})


// Evento click para eliminar una compra
$(document).on("click", ".boton-eliminar-compra", function () {
    const _compra = $(this).data("dataCompra");

    Swal.fire({
        title: '¿Estás seguro?',
        text: "Esta acción eliminará el Detalle Compra. No podrás deshacerla.",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, eliminar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            // Llama a la función para eliminar la compra
            eliminarCompra(_compra.idDetalleCompra);
        }
    });
});


// Función para eliminar una compra por ID
function eliminarCompra(idDetalleCompra) {
    fetch(`/Home/eliminarDetalleCompra?idDetalleCompra=${idDetalleCompra}`, {
        method: "PUT"
    })
        .then(response => {
            return response.ok ? response.json() : Promise.reject(response)
        })
        .then(responseJson => {
            // Muestra un mensaje de éxito o error
            if (responseJson.valor) {
                Swal.fire("Eliminado!", "El Detalle-Compra fue eliminada correctamente.", "success");
                MostrarCompra();
            }
            else {
                Swal.fire("Error", "No se pudo eliminar el Detalle-Compra.", "error");
            }
        })
        .catch(error => {
            Swal.fire("Error", "Ocurrió un error al eliminar el Detalle-Compra.", "error");
        });
}