﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using EHRRepository;
using EHRDomain;

namespace EHRApp
{
    public sealed class CTImageApp
    {
        private readonly CTImageRepository m_cTImageRepository;

        public CTImageApp(CTImageRepository cTImageRepository)
        {
            m_cTImageRepository = cTImageRepository;
        }

        public async Task<List<CTImage>> GetListByPatientCaseId(int patientCaseId)
        {
            List<CTImage> cTImages = await m_cTImageRepository.GetListByPatientCaseId(patientCaseId)
                    .ToListAsync().ConfigureAwait(false);
            if (cTImages != null) cTImages.ForEach(item => item.PatientCaseId = patientCaseId);
            return cTImages;
        }
    }
}
