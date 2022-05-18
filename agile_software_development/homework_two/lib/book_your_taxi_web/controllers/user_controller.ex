defmodule BookYourTaxiWeb.UserController do
  use BookYourTaxiWeb, :controller

  import Ecto.Query

  alias BookYourTaxi.Repo
  alias BookYourTaxi.Sales.Taxi
  alias BookYourTaxi.Accounts.User

  def new(conn, _params) do
    changeset = User.changeset(%User{}, %{})
    render(conn, "new.html", changeset: changeset)
  end

  def show(conn, %{"id" => id}) do
    user = Repo.get!(User, id)

    taxi =
      from(t in Taxi, where: t.user_id == ^id)
      |> Repo.one()
      |> Repo.preload(user: :taxi)

    case taxi != nil do
      true  -> render(conn, "show.html", user: user, taxi: taxi, has_taxi: true)
      false -> render(conn, "show.html", user: user, has_taxi: false)
    end
  end

  def create(conn, %{"user" => user_params}) do
    changeset = User.changeset(%User{}, user_params)

    case Repo.insert(changeset) do
      {:ok, _user} ->
        conn
        |> put_flash(:info, "User created successfully.")
        |> redirect(to: Routes.session_path(conn, :new))
      {:error, changeset} ->
        render(conn, "new.html", changeset: changeset)
    end
  end
end
