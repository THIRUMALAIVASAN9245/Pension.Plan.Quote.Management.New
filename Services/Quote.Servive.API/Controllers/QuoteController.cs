namespace Quote.Servive.API.Controllers
{
    using AutoMapper;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Quote.Service.API.Domain.Entities;
    using Quote.Service.API.Models.ResponseModels;
    using Quote.Service.API.Models.ResquestModels;
    using Quote.Service.API.Service.RequestHandlers.CommandHandlers;
    using Quote.Service.API.Service.RequestHandlers.QueryHandlers;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [ApiController]
    [Route("[controller]")]
    public class QuoteController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public QuoteController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        /// <summary>
        /// Action to create a new quote in the database.
        /// </summary>
        /// <param name="createQuoteModel">Model to create a new quote</param>
        /// <returns>Returns the created quote</returns>
        /// <response code="201">Returned if the quote was created</response>
        /// <response code="400">Returned if the model couldn't be parsed or the quote couldn't be saved</response>
        /// <response code="422">Returned when the validation failed</response>
        /// <response code="409">Returned when the mediator failed</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ResponseQuoteModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateQuoteModel createQuoteModel)
        {
            try
            {
                var quote = _mapper.Map<Quote>(createQuoteModel);
                var response = await _mediator.Send(new CreateQuoteRequest(quote));

                if (response == null)
                {
                    return StatusCode(409, "Error Occurred While Creating a Quote");
                }

                var quoteModel = _mapper.Map<ResponseQuoteModel>(response);

                return Created("New Quote Created", quoteModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Action to update an existing quote
        /// </summary>
        /// <param name="updateQuoteModel">Model to update an existing quote</param>
        /// <returns>Returns the updated quote</returns>
        /// /// <response code="200">Returned if the quote was updated</response>
        /// /// <response code="400">Returned if the model couldn't be parsed or the quote couldn't be found</response>
        /// /// <response code="422">Returned when the validation failed</response>
        /// /// <response code="409">Returned when the mediator failed</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseQuoteModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateQuoteModel updateQuoteModel)
        {
            try
            {
                var quote = await _mediator.Send(new GetQuoteByIdRequest(updateQuoteModel.Id));
                if (quote == null)
                {
                    return BadRequest($"No quote found with the id {updateQuoteModel.Id}");
                }

                var quoteEntity = _mapper.Map(updateQuoteModel, quote);
                await _mediator.Send(new UpdateQuoteRequest(quoteEntity));

                var quoteModel = _mapper.Map<ResponseQuoteModel>(quoteEntity);

                return Ok(quoteModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Action to delete an existing quote
        /// </summary>
        /// <param name="quoteId">quoteId</param>
        /// <returns>Returns the delete quote</returns>
        /// /// <response code="200">Returned if the quote was deleted</response>
        /// /// <response code="400">Returned if the model couldn't be parsed or the quote couldn't be found</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseQuoteModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid quoteId)
        {
            try
            {
                var quote = await _mediator.Send(new GetQuoteByIdRequest(quoteId));
                if (quote == null)
                {
                    return BadRequest($"No quote found with the id {quoteId}");
                }

                await _mediator.Send(new DeleteQuoteRequest(quote));

                var quoteModel = _mapper.Map<ResponseQuoteModel>(quote);

                return Ok(quoteModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Action to get quote list
        /// </summary>
        /// <param name="getQuoteRequestModel">Model to get quote list</param>
        /// <returns>Returns the updated quote</returns>
        /// /// <response code="200">Returned if the get quote list was avalaible</response>
        /// /// <response code="400">Returned if the model couldn't be parsed or the quote list couldn't be found</response>
        /// /// <response code="404">Returned when the mediator failed</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ResponseQuoteModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await _mediator.Send(new GetQuoteRequest());
                if (response == null)
                {
                    return NotFound("No Quote found");
                }

                var responseList = _mapper.Map<List<ResponseQuoteModel>>(response);

                return Ok(responseList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}