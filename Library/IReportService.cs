using Itacometragem.Models;
using System.Collections.Generic;

namespace Itacometragem.Library
{
    public interface IReportService
    {
        public byte[] GeneratePdfReport(Report report);
    }
}
