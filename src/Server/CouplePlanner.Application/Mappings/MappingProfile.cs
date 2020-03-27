using System;
using AutoMapper;
using CouplePlanner.Application.Entities;

namespace CouplePlanner.Application.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Domain.Entities.Event, Event>()
			  .ForMember(e => e.StartDate,
				options => options.MapFrom(e => new DateTimeOffset(e.StartDate, TimeSpan.Zero)))
			  .ForMember(e => e.EndDate,
				options => options.MapFrom(e => new DateTimeOffset(e.EndDate, TimeSpan.Zero)));

			CreateMap<Event, Domain.Entities.Event>()
			  .ForMember(e => e.StartDate,
				options => options.MapFrom(e => e.StartDate.UtcDateTime))
			  .ForMember(e => e.EndDate,
				options => options.MapFrom(e => e.EndDate.UtcDateTime));

			CreateMap<Domain.Entities.Absence, Absence>()
			  .ForMember(a => a.StartDate,
				options => options.MapFrom(a => new DateTimeOffset(a.StartDate, TimeSpan.Zero)))
			  .ForMember(a => a.EndDate,
				options => options.MapFrom(a => new DateTimeOffset(a.EndDate, TimeSpan.Zero)));

			CreateMap<Absence, Domain.Entities.Absence>()
			  .ForMember(a => a.StartDate,
				options => options.MapFrom(a => a.StartDate.UtcDateTime))
			  .ForMember(a => a.EndDate,
				options => options.MapFrom(a => a.EndDate.UtcDateTime));
		}
	}
}
