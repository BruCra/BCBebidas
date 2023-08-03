using ApiBCB.AD.Models.Categorias;
using ApiBCB.AD.Models.Productos;
using ApiBCB.AD.Services.Categorias;
using ApiBCB.AD.Services.Productos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiBCB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly S_ICategorias _icategorias;

        public CategoriasController(S_ICategorias icategorias)
        {
            _icategorias = icategorias;
        }

        [Authorize, HttpGet("GetCategorias")]
        public async Task<ActionResult> GetCategorias()
        {
            try
            {
                var m_Categorias = await _icategorias.GetCategoriasAsync();

                if (m_Categorias != null)
                {
                    return Ok(m_Categorias);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Controller: Se produjo un error al obtener las categorias " + ex.Message);
            }
        }

        [Authorize, HttpGet("{Id_Categoria}")]
        public async Task<ActionResult> GetCategoriasById(int Id_Categoria)
        {
            try
            {
                if (Id_Categoria != 0)
                {
                    var m_Categorias = await _icategorias.GetCategoriasByIdAsync(Id_Categoria);

                    if (m_Categorias != null)
                    {
                        return Ok(m_Categorias);
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
                throw new Exception("Controller: Se produjo un error al obtener la categoria por Id " + ex.Message);
            }
        }

        [Authorize, HttpPost("AddModifyCategorias")]
        public async Task<ActionResult> AddModifyCategorias([FromBody] M_Categorias categorias)
        {
            try
            {
                if (categorias != null)
                {
                    M_Categorias m_Categorias = new();

                    if (categorias.Id_Categoria == 0)
                    {
                        m_Categorias = await _icategorias.GetCategoriasByNameAsync(categorias.NombreCategoria);

                        if (m_Categorias == null)
                        {
                            var Id_Categoria = await _icategorias.AddCategoriasAsync(categorias);
                            return Ok(Id_Categoria);
                        }
                        else
                        {
                            ModelState.AddModelError("Advertencia Agregar", "La categoria con ese nombre ya existe!");
                            return BadRequest(ModelState);
                        }
                    }
                    else
                    {
                        m_Categorias = await _icategorias.GetCategoriasByIdAsync(categorias.Id_Categoria);

                        if (m_Categorias == null)
                        {
                            ModelState.AddModelError("Advertencia Modificar", "No se encuentra la categoria a modificar!");
                            return BadRequest(ModelState);
                        }

                        m_Categorias = await _icategorias.GetCategoriasByNameAsync(categorias.NombreCategoria);

                        if (m_Categorias != null && m_Categorias.Id_Categoria != categorias.Id_Categoria && m_Categorias.NombreCategoria == categorias.NombreCategoria)
                        {
                            ModelState.AddModelError("Advertencia Modificar", "La categoira con ese nombre ya existe!");
                            return BadRequest(ModelState);
                        }
                        else
                        {
                            var rowAffected = await _icategorias.ModifyCategoriasAsync(categorias);

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
                    return BadRequest(categorias);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Controller: Se produjo un error al agregar/modificar una categoria " + ex.Message);
            }

        }
    }
}
