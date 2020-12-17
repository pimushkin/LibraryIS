﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LibraryIS.Application.Mappings;
using LibraryIS.Core.Entities;

namespace LibraryIS.Application.DTOs
{
    public class BookPreviewDto : IMapFrom<Book>
    {
        public string Name { get; set; }
        public List<string> Authors { get; set; }
        public DateTime PublicationDate { get; set; }
        public int PageCount { get; set; }
        public List<string> PublishingHouses { get; set; }
        public List<string> Genres { get; set; }
        public double? Rating { get; set; }

        public void Mapping(Profile profile)
        {
                profile.CreateMap<Book, BookPreviewDto>()
                .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.Authors.Select(x => x.Name)))
                .ForMember(dest => dest.PublishingHouses, opt => opt.MapFrom(src => src.PublishingHouses.Select(x => x.Name)))
                .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(x => x.Name)));
        }
    }
}