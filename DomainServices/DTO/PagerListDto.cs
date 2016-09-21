using System.Collections.Generic;

namespace SSU.ITA.WorkFlow.Domain.Services.DTO
{
    public interface IPagerListDto
    {

    }

    public class PagerListDto : IPagerListDto
    {
        public int TotalCount { get; set; }
        public IEnumerable<IProjectDto> Items { get; set; }          
    }
}
