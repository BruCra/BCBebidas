using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiBCB.AD.Models.Productos;
using ApiBCB.AD.Services.Productos;
using Microsoft.AspNetCore.Authorization;

namespace ApiBCB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly S_IProductos _iproductos;

        public ProductosController(S_IProductos iproductos)
        {
            _iproductos = iproductos;
        }

        [Authorize, HttpGet("GetProductos")]
        public async Task<ActionResult> GetProductos()
        {
            try
            {
                var m_Productos = await _iproductos.GetProductosAsync();

                if (m_Productos != null)
                {
                    return Ok(m_Productos);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Controller: Se produjo un error al obtener los productos " + ex.Message);
            }
        }

        [Authorize, HttpGet("{Id_Producto}")]
        public async Task<ActionResult> GetProductosById(int Id_Producto)
        {
            try
            {
                if (Id_Producto != 0)
                {
                    var m_Productos = await _iproductos.GetProductosByIdAsync(Id_Producto);

                    if (m_Productos != null)
                    {
                        return Ok(m_Productos);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Controller: Se produjo un error al obtener el producto por Id " + ex.Message);
            }
        }

        [Authorize, HttpPost("AddModifyProductos")]
        public async Task<ActionResult> AddModifyProductos([FromBody] M_Productos productos)
        {
            try
            {
                if (productos != null)
                {
                    M_Productos m_Productos = new();

                    if (productos.Id_Producto == 0)
                    {
                        m_Productos = await _iproductos.GetProductosByNameAsync(productos.NombreProducto);

                        if (m_Productos == null)
                        {
                            var Id_Producto = await _iproductos.AddProductosAsync(productos);
                            return Ok(Id_Producto);
                        }
                        else
                        {
                            ModelState.AddModelError("Advertencia Agregar", "El producto con ese nombre ya existe!");
                            return BadRequest(ModelState);
                        }
                    }
                    else
                    {
                        m_Productos = await _iproductos.GetProductosByIdAsync(productos.Id_Producto);

                        if (m_Productos == null)
                        {
                            ModelState.AddModelError("Advertencia Modificar", "No se encuentra el producto a modificar!");
                            return BadRequest(ModelState);
                        }

                        m_Productos = await _iproductos.GetProductosByNameAsync(productos.NombreProducto);

                        if (m_Productos != null && m_Productos.Id_Producto != productos.Id_Producto && m_Productos.NombreProducto == productos.NombreProducto)
                        {
                            ModelState.AddModelError("Advertencia Modificar", "El producto con ese nombre ya existe!");
                            return BadRequest(ModelState);
                        }
                        else
                        {
                            var rowAffected = await _iproductos.ModifyProductosAsync(productos);

                            if (rowAffected != 0)
                            {
                                return Ok(rowAffected);
                            }
                            else
                            {
                                return NotFound();
                            }
                        }
                    }
                }
                {
                    return BadRequest(productos);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Controller: Se produjo un error al agregar/modificar un producto " + ex.Message);
            }

        }
    }
}
