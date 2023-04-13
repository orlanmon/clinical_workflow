using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clinicalworkflow.web.services.dto
{
    static public class AutoMapperConfiguration
    {

        public static MapperConfiguration AutoMapperConfig = null;

        public static void Configure()
        {
            AutoMapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<clinicalworkflow.web.services.model.Models.DB.ClinicalDataFieldFourLookup, clinicalworkflow.web.services.dto.ClinicalDataFieldFourLookupDTO>();
                cfg.CreateMap< clinicalworkflow.web.services.dto.ClinicalDataFieldFourLookupDTO, clinicalworkflow.web.services.model.Models.DB.ClinicalDataFieldFourLookup>();
                cfg.CreateMap<clinicalworkflow.web.services.model.Models.DB.ClinicalDataFieldSixLookup, clinicalworkflow.web.services.dto.ClinicalDataFieldSixLookupDTO>();
                cfg.CreateMap<clinicalworkflow.web.services.dto.ClinicalDataFieldSixLookupDTO, clinicalworkflow.web.services.model.Models.DB.ClinicalDataFieldSixLookup >();
                cfg.CreateMap<clinicalworkflow.web.services.model.Models.DB.Patient, clinicalworkflow.web.services.dto.PatientDTO>();
                cfg.CreateMap<clinicalworkflow.web.services.dto.PatientDTO, clinicalworkflow.web.services.model.Models.DB.Patient>();
                cfg.CreateMap<clinicalworkflow.web.services.model.Models.DB.PatientClinicalData, clinicalworkflow.web.services.dto.PatientClinicalDataDTO>();
                cfg.CreateMap<clinicalworkflow.web.services.dto.PatientClinicalDataDTO, clinicalworkflow.web.services.model.Models.DB.PatientClinicalData>();
                cfg.CreateMap<clinicalworkflow.web.services.model.Models.DB.UserLogin, clinicalworkflow.web.services.dto.UserLoginDTO>();
                cfg.CreateMap<clinicalworkflow.web.services.dto.UserLoginDTO, clinicalworkflow.web.services.model.Models.DB.UserLogin>();
            });
        }


    }
}
