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
        private readonly HttpClient httpClient;
        public TaskDataService(IHttpClientFactory clientFactory)
        {
            httpClient = clientFactory.CreateClient("FamilyTaskAPI");
            tasks = new List<TaskVm>();
            Loadtasks();
        }

        private async void Loadtasks()
        {
            tasks = (await GetAllTasks()).Payload;
            TasksUpdated?.Invoke(this, null);
        }

        private IEnumerable<TaskVm> tasks;
        public IEnumerable<TaskVm> Tasks => tasks;

        public TaskVm SelectedTask { get; private set; }


        public event EventHandler TasksUpdated;
        public event EventHandler TaskSelected;

        private async Task<CreateTaskCommandResult> Create(CreateTaskCommand command)
        {
            return await httpClient.PostJsonAsync<CreateTaskCommandResult>("/api/tasks", command);
        }

        private async Task<GetAllTasksQueryResult> GetAllTasks()
        {
            return await httpClient.GetJsonAsync<GetAllTasksQueryResult>("tasks");
        }

        private async Task<ToggleTaskCommandResult> Toggle(Guid id)
        {
            return await httpClient.GetJsonAsync<ToggleTaskCommandResult>($"/api/Tasks/{id}/toggle-complete");
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
    }
}