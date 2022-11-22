﻿using System.Net.Http.Json;
using System.Text.Json;
using Elearn.HttpClients.Service;
using Elearn.Shared.Dtos;
using Elearn.Shared.Models;


namespace Elearn.Clients.Http;

public class LectureHttpClient : ILectureService
{
    
    private readonly HttpClient client;

    public LectureHttpClient(HttpClient client)
    {
        this.client = client;
    }
    public async Task<LectureDto> CreateAsync(LectureCreationDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/lectures",dto);
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            
            throw new Exception(content);
        }
        LectureDto lectureDto = JsonSerializer.Deserialize<LectureDto>(content, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return lectureDto;
    }

    public async Task<List<LectureDto>> GetLecturesAsync()
    {
        
        HttpResponseMessage response = await client.GetAsync("/lectures");
        string result = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(result);
        }

        List<LectureDto> lectureDtos = JsonSerializer.Deserialize<List<LectureDto>>(result, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        })!;
        return lectureDtos;
    }

    public async Task<LectureDto?> GetLectureByUrlAsync(string url)
    {
        HttpResponseMessage response = await client.GetAsync($"/lectures/{url}");
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
        return await response.Content.ReadFromJsonAsync<LectureDto>();
    }
}