defmodule BookYourTaxiWeb.TaxiController do
  use BookYourTaxiWeb, :controller

  alias BookYourTaxi.{Repo}

  def new(conn, _params) do
    render(conn, "new.html")
  end

  def transform("rate", value), do: String.to_float(value)
  def transform("number_of_seats", value), do: String.to_integer(value)
  def transform(_, value), do: value

  def create(conn, taxi_params) do
    user = conn.assigns.current_user
    taxi_struct = Ecto.build_assoc(user, :taxi, Enum.map(taxi_params, fn({key, value}) -> {String.to_atom(key), transform(key, value)} end))

    case Repo.insert(taxi_struct) do
      {:ok, _taxi} ->
        conn
        |> put_flash(:info, "Taxi successfully created.")
        |> redirect(to: Routes.user_path(conn, :show, user))
      {:error, _taxi} ->
        render(conn, "new.html")
    end
  end

end
