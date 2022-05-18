defmodule BookYourTaxi.Repo do
  use Ecto.Repo,
    otp_app: :book_your_taxi,
    adapter: Ecto.Adapters.Postgres
end
