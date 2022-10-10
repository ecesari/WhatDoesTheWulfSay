using System.ComponentModel.DataAnnotations;

namespace WhatDoesTheWulfSay.Domain.Entities
{
    public class BaseEntity
	{
		[Key]
		public Guid Id { get; set; }
	}
}
