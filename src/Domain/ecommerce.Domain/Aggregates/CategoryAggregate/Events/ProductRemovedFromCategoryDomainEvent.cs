﻿using ecommerce.Domain.Aggregates.CategoryAggregate.ValueObjects;
using ecommerce.Domain.Aggregates.ProductAggregate.ValueObjects;
using ecommerce.Domain.Common;

namespace ecommerce.Domain.Aggregates.CategoryAggregate.Events;
public sealed record ProductRemovedFromCategoryDomainEvent(CategoryId CategoryId, ProductId ProductId) : DomainEvent;