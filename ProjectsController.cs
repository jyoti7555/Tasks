using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjectService.WebAPI.Data;
using ProjectService.WebAPI.Services;
using System.Net;
using System;
using ProjectService.WebAPI.Entities;
using ProjectService.WebAPI.BusinessLogic;

namespace ProjectService.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        IUsersService objUserServ;
        IProjectsService objProjServ;
        public ProjectsController(IUsersService iUsersService, IProjectsService iProjectService)
        {
            objUserServ = iUsersService;
            objProjServ = iProjectService;
        }

        [HttpPost]
        public IActionResult PostAsync(Project project)
        {
            IResult result = new Result();
            try
            {
                if (project != null)
                {
                     objProjServ.Add(project);
                }
                else
                    result.Status = HttpStatusCode.BadRequest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return StatusCode((int)result.Status);
        }


        [Route("{projectId:int}/users")]
        [HttpGet]
        public IActionResult Get(int projectId)
        {
            IResult result = new Result(); try
            {
                int[] ids = new int[] { projectId };
                var varProList = objProjServ.Get(ids);
                if (varProList != null && varProList.Result.Count() > 0)
                {
                    return Ok(objUserServ.Get2(projectId).Result);
                }
                else
                    result.Status = HttpStatusCode.NotFound;
            }
            catch (Exception ex) {
                throw ex;
            }
            return StatusCode((int)result.Status);
        }

        [Route("{projectId:int}/users")]
        [HttpPost]
        public IActionResult PostAsync(User user)
        {
            IResult result = new Result();
            try
            {
                if (user != null)
                {
                    int[] ids = new int[] { user.ProjectId };
                    var varProList = objProjServ.Get(ids).Result;
                    if (varProList.Count() > 0)
                    {
                        objUserServ.Add(user);
                        result.Status = HttpStatusCode.Created;
                    }
                    else
                        result.Status = HttpStatusCode.NotFound;
                }
                else
         
                    
                    
                    
                   result.Status = HttpStatusCode.BadRequest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return StatusCode((int)result.Status);
        }

        [Route("{projectId:int}")]
        [HttpDelete]
        public IActionResult Delete(int projectId)
        {
            IResult result = new Result();
            try
            {
                int[] ids = new int[] { projectId };
                var varProList = objProjServ.Get(ids).Result;
                if (varProList.Count() > 0)
                {
                    objProjServ.Delete(varProList.First());
                    result.Status = HttpStatusCode.NoContent;
                }
                else
                    result.Status = HttpStatusCode.NotFound;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return StatusCode((int)result.Status);
        }
    }
}