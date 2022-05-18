defmodule Exam.Application do
  # See https://hexdocs.pm/elixir/Application.html
  # for more information on OTP Applications
  @moduledoc false

  use Application

  @impl true
  def start(_type, _args) do
    children = [
      # Start the Ecto repository
      Exam.Repo,
      # Start the Telemetry supervisor
      ExamWeb.Telemetry,
      # Start the PubSub system
      {Phoenix.PubSub, name: Exam.PubSub},
      # Start the Endpoint (http/https)
      ExamWeb.Endpoint
      # Start a worker by calling: Exam.Worker.start_link(arg)
      # {Exam.Worker, arg}
    ]

    # See https://hexdocs.pm/elixir/Supervisor.html
    # for other strategies and supported options
    opts = [strategy: :one_for_one, name: Exam.Supervisor]
    Supervisor.start_link(children, opts)
  end

  # Tell Phoenix to update the endpoint configuration
  # whenever the application is updated.
  @impl true
  def config_change(changed, _new, removed) do
    ExamWeb.Endpoint.config_change(changed, removed)
    :ok
  end
end
