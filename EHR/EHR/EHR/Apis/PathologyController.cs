﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EHR.Models;
using EHRApp;
using EHRDomain;
using EHRUtil;

namespace EHR.Apis
{
    [ApiController]
    public class PathologyController : ControllerBase
    {
        private PathologyApp m_pathologyApp;

        public PathologyController(PathologyApp pathologyApp)
        {
            m_pathologyApp = pathologyApp;
        }

        [Route("api/pathology/getonebyid")]
        [HttpPost]
        public async Task<OutputBaseViewModel> GetOneById([FromForm]int id)
        {
            ResultViewModel<Pathology> result = new ResultViewModel<Pathology>();
            try
            {
                result.Data = await m_pathologyApp.GetOneByIdAsync(id).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                result.Status = 100101;
                result.Message = ex.GetMessage();
            }
            return result;
        }

        [HttpPost]
        [Route("api/pathology/updateClinicalNotes")]
        public async Task<OutputBaseViewModel> UpdateClinicalNotesAsync([FromForm]int id,[FromForm] string clinicalNotes)
        {
            ResultViewModel<Pathology> result = new ResultViewModel<Pathology>();
            try
            {
                result.Data = await m_pathologyApp.UpdateClinicalNotesAsync(id, clinicalNotes).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                result.Status = 100103;
                result.Message = ex.GetMessage();
            }
            return result;
        }
    }
}
