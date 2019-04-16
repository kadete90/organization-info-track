using System.Linq;
using AutoMapper;
using Humanizer;
using InfoTrack.Common.Contracts;
using InfoTrack.DAL.Entities;

namespace InfoTrack.App.Utils
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SearchHistory, SearchHistoryModel>()
                .ForMember(dest => dest.Keyword, source => source.MapFrom(src => src.Keyword))
                .ForMember(dest => dest.SearchDate, source => source.MapFrom(src => src.SearchDate.Humanize(true, null, null)))
                .ForMember(dest => dest.Url, source => source.MapFrom(src => src.Url))
                .ForMember(dest => dest.MatchEntries, 
                                    source => source.MapFrom(src => src.SearchMatches.Any() 
                                            ? string.Join(", ", src.SearchMatches.Select(s => s.Entry))
                                            : "0"));
        }
    }
}
