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
    public sealed class RadiologySharedNotesApp
    {
        private readonly PatientCaseRepository m_patientCaseRepository;
        private readonly RadiologySharedNotesRepository m_radiologySharedNotesRepository;

        public RadiologySharedNotesApp(PatientCaseRepository patientCaseRepository,RadiologySharedNotesRepository radiologySharedNotesRepository)
        {
            m_patientCaseRepository = patientCaseRepository;
            m_radiologySharedNotesRepository = radiologySharedNotesRepository;
        }

        public async Task<RadiologySharedNotes> GetOneByIdAsync(int id)
        {
            RadiologySharedNotes radiologySharedNotes = await m_radiologySharedNotesRepository.GetOneById(id)
                    .AsNoTracking().FirstOrDefaultAsync();
            return radiologySharedNotes;
        }

        public async Task<RadiologySharedNotes> AddAsync(RadiologySharedNotes radiologySharedNotes)
        {
            PatientCase patientCase = await m_patientCaseRepository.GetOne(radiologySharedNotes.PatientCaseId).FirstOrDefaultAsync();
            patientCase.RadiologySharedNotes = radiologySharedNotes;
            await m_radiologySharedNotesRepository.SaveChangesAsync();
            return radiologySharedNotes;
        }

        public async Task UpdateAsync(RadiologySharedNotes radiologySharedNotes)
        {
            RadiologySharedNotes temp = await m_radiologySharedNotesRepository.GetOneById(radiologySharedNotes.Id)
                    .FirstOrDefaultAsync().ConfigureAwait(false);
            if (temp == null) throw new ArgumentException("radiologySharedNotes not exists");
            temp.SharedNotes = radiologySharedNotes.SharedNotes;
            await m_radiologySharedNotesRepository.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}
