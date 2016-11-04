using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using System.IO;
using GeoLocation.Importer.Models;
using GeoLocation.Importer.Processors;
using GeoLocation.Importer.Enums;

namespace GeoLocation.Importer.Services
{
    public class ImportService
    {
        public bool RunImport(string fileToImport, ImportOptionEnum importOption, out string message)
        {
            var success = false;

            try
            {
                switch (importOption)
                {
                    case ImportOptionEnum.GeoLite2CountryLocations:
                        success = this.GeoLite2CountryLocationsImport(fileToImport);
                        break;
                    case ImportOptionEnum.GeoLite2CountryBlocks:
                        success = this.GeoLite2CountryBlockImport(fileToImport);
                        break;
                    default:
                        break;
                }

                if (success)
                {
                    message = "File successfully imported!";
                }
                else
                {
                    message = "An Error Occurred importing the file!";
                }
            }
            catch (Exception e)
            {
                message = string.Format("An Error Occurred importing file: {0}", e.Message);
            }

            return success;
        }

        public bool DeleteElasticSearchIndex(out string message)
        {
            return new GeoLite2CountryLocationsProcessor().DeleteIndex(out message);
        }

        private bool ImportData(IBaseProcessor processor)
        {
            return processor.Import();
        }

        private bool GeoLite2CountryLocationsImport(string fileToImport)
        {
            // Convert csv into enumerable of a model.
            List<GeoLite2CountryLocationsDefinition> data = new List<GeoLite2CountryLocationsDefinition>();

            using (TextReader reader = File.OpenText(fileToImport))
            {
                var csv = new CsvReader(reader);
                csv.Configuration.RegisterClassMap<GeoLite2CountryLocationsDefinitionMap>();
                while (csv.Read())
                {
                    data.Add(csv.GetRecord<GeoLite2CountryLocationsDefinition>());
                }
            }

            // process data
            if (data.Count() > 0)
            {
                return this.ImportData(new GeoLite2CountryLocationsProcessor(data));
            }

            return false;
        }

        private bool GeoLite2CountryBlockImport(string fileToImport)
        {
            // Convert csv into enumerable of a model.
            List<GeoLite2CountryBlockDefinition> data = new List<GeoLite2CountryBlockDefinition>();

            using (TextReader reader = File.OpenText(fileToImport))
            {
                var csv = new CsvReader(reader);
                csv.Configuration.RegisterClassMap<GeoLite2CountryBlockDefinitionMap>();
                while (csv.Read())
                {
                    data.Add(csv.GetRecord<GeoLite2CountryBlockDefinition>());
                }
            }

            // process data
            if (data.Count() > 0)
            {
                return this.ImportData(new GeoLite2CountryBlockProcessor(data));
            }

            return false;
        }
    }
}
