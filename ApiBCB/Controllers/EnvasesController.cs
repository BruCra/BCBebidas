using ApiBCB.AD.Models.Envases;
using ApiBCB.AD.Models.Productos;
using ApiBCB.AD.Services.Envases;
using ApiBCB.AD.Services.Productos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiBCB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvasesController : ControllerBase
    {
        private readonly S_IEnvases _ienvases;

        public EnvasesController(S_IEnvases ienvases)
        {
            _ienvases = ienvases;
        }

        [Authorize, HttpGet("GetEnvases")]
        public async Task<ActionResult> GetEnvases()
        {
            try
            {
                var m_Envases = await _ienvases.GetEnvasesAsync();

                if (m_Envases != null)
                {
                    return Ok(m_Envases);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Controller: Se produjo un error al obtener los envases " + ex.Message);
            }
        }

        [Authorize, HttpGet("{Id_Envase}")]
        public async Task<ActionResult> GetEnvasesById(int Id_Envase)
        {
            try
            {
                if (Id_Envase != 0)
                {
                    var m_Envases = await _ienvases.GetEnvasesByIdAsync(Id_Envase);

                    if (m_Envases != null)
                    {
                        return Ok(m_Envases);
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
                throw new Exception("Controller: Se produjo un error al obtener el envase por Id " + ex.Message);
            }
        }

        [Authorize, HttpPost("AddModifyEnvases")]
        public async Task<ActionResult> AddModifyEnvases([FromBody] M_Envases envases)
        {
            try
            {
                if (envases != null)
                {
                    M_Envases m_Envases = new();

                    if (envases.Id_Envase == 0)
                    {
                        m_Envases = await _ienvases.GetEnvasesByNameAsync(envases.Milimetros);

                        if (m_Envases == null)
                        {
                            var Id_Envase = await _ienvases.AddEnvasesAsync(envases);
                            return Ok(Id_Envase);
                        }
                        else
                        {
                            ModelState.AddModelError("Advertencia Agregar", "El envase con ese nombre ya existe!");
                            return BadRequest(ModelState);
                        }
                    }
                    else
                    {
                        m_Envases = await _ienvases.GetEnvasesByIdAsync(envases.Id_Envase);

                        if (m_Envases == null)
                        {
                            ModelState.AddModelError("Advertencia Modificar", "No se encuentra el envase a modificar!");
                            return BadRequest(ModelState);
                        }

                        m_Envases = await _ienvases.GetEnvasesByNameAsync(envases.Milimetros);

                        if (m_Envases != null && m_Envases.Id_Envase != envases.Id_Envase && m_Envases.Milimetros == envases.Milimetros)
                        {
                            ModelState.AddModelError("Advertencia Modificar", "El envase con ese nombre ya existe!");
                            return BadRequest(ModelState);
                        }
                        else
                        {
                            var rowAffected = await _ienvases.ModifyEnvasesAsync(envases);

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
                    return BadRequest(envases);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Controller: Se produjo un error al agregar/modificar un envase " + ex.Message);
            }

        }
    }
}
