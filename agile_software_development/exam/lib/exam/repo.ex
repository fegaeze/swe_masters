defmodule Exam.Repo do
  use Ecto.Repo,
    otp_app: :exam,
    adapter: Ecto.Adapters.Postgres
end
