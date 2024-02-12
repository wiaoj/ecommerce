using ecommerce.Application.Common.Interfaces;
using ecommerce.Application.Features.Authentication.Common;
using MediatR;

namespace ecommerce.Application.Features.Authentication.Commands.Register;
public sealed record RegisterCommand(String FirstName,
                                     String? MidlleNames,
                                     String LastName,
                                     String Email,
                                     String? PhoneNumber,
                                     String Password) : IRequest<AuthenticationResponse>, IHasTransaction, IHasEvent;