using Core.Extensions.ModelConversion;
using Domain.Commands;
using Domain.Queries;
using Domain.ViewModel;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebClient.Abstractions;
using WebClient.Shared.Models;

namespace WebClient.Services
{
    public class TaskDataService : ITaskDataService
    {
        private readonly HttpClient _httpClient;
        public TaskDataService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("FamilyTaskAPI");
            _tasks = new List<TaskVm>();
            Loadtasks();
        }

        private async void Loadtasks()
        {
            _tasks = (await GetAllTasks()).Payload;
            TasksUpdated?.Invoke(this, null);
        }

        private IEnumerable<TaskVm> _tasks;
        public IEnumerable<TaskVm> Tasks => _tasks;

        public TaskVm SelectedTask { get; private set; }


        public event EventHandler TasksUpdated;

        private async Task<CreateTaskCommandResult> Create(CreateTaskCommand command)
        {
            return await _httpClient.PostJsonAsync<CreateTaskCommandResult>("/api/tasks", command);
        }

        private async Task<GetAllTasksQueryResult> GetAllTasks()
        {
            return await _httpClient.GetJsonAsync<GetAllTasksQueryResult>("tasks");
        }

        private async Task<ToggleTaskCommandResult> Toggle(Guid id)
        {
            return await _httpClient.GetJsonAsync<ToggleTaskCommandResult>($"/api/Tasks/{id}/toggle-complete");
        }

        private async Task<AssignMemberCommandResult> Assign(AssignMemberCommand command)
        {
            return await _httpClient.PostJsonAsync<AssignMemberCommandResult>("/api/Tasks/assign-member", command);
        }

        public void SelectTask(Guid id)
        {
            SelectedTask = Tasks.SingleOrDefault(t => t.Id == id);
            TasksUpdated?.Invoke(this, null);
        }

        public async Task ToggleTask(Guid id)
        {
            var result = await Toggle(id);
            if(result != null)
                Loadtasks();
            
            TasksUpdated?.Invoke(this, null);
        }

        public async Task AddTask(CreateTaskVm model)
        {
            var result = await Create(model.ToCreateTaskCommand());
            if(result != null)
                Loadtasks();

            TasksUpdated?.Invoke(this, null);
        }

        public async Task AssignTask(TaskVm model)
        {
            var result = await Assign(model.ToAssignMemberCommand());
            if(result != null && result.Succeed)
                Loadtasks();

            TasksUpdated?.Invoke(this, null);
        }
    }
}