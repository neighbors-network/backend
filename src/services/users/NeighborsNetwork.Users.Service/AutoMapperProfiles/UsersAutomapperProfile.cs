using System;
using AutoMapper;
using JetBrains.Annotations;
using NeighborsNetwork.Users.Db.Entities;
using NeighborsNetwork.Users.Service.Processing.Responses;

namespace NeighborsNetwork.Users.Service.AutoMapperProfiles
{
    [UsedImplicitly]
    internal class DateOnlyToDateTimeOffsetConverter : ITypeConverter<DateOnly, DateTimeOffset>
    {
        public DateTimeOffset Convert(DateOnly source, DateTimeOffset destination, ResolutionContext context)
        {
            return new DateTimeOffset(new DateTime(source.Year, source.Month, source.Day, 0, 0, 0, DateTimeKind.Utc));
        }
    }

    [UsedImplicitly]
    internal class DateTimeOffsetToDateOnlyConverter : ITypeConverter<DateTimeOffset, DateOnly>
    {
        public DateOnly Convert(DateTimeOffset source, DateOnly destination, ResolutionContext context)
        {
            var date = source.UtcDateTime;
            return new DateOnly(date.Year, date.Month, date.Day);
        }
    }

    [UsedImplicitly]
    public class UsersAutomapperProfile : Profile
    {
        public UsersAutomapperProfile()
        {
            CreateMap<DateOnly, DateTimeOffset>().ConvertUsing<DateOnlyToDateTimeOffsetConverter>();
            CreateMap<DateTimeOffset, DateOnly>().ConvertUsing<DateTimeOffsetToDateOnlyConverter>();

            CreateMap<UserDto, User>().ReverseMap();
        }
    }
}
