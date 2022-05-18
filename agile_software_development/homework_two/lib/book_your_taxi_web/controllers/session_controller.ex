defmodule BookYourTaxiWeb.SessionController do
  use BookYourTaxiWeb, :controller

  alias BookYourTaxi.{Authentication, Repo}
  alias BookYourTaxi.Accounts.User

  def new(conn, _params) do
    render conn, "new.html"
  end

  def delete(conn, _params) do
    conn
    |> Authentication.logout()
    |> redirect(to: Routes.session_path(conn, :new))
  end


  def create(conn, %{"session" => %{"email" => email, "password" => password}}) do
    user = Repo.get_by(User, email: email)

    case Authentication.check_credentials(conn, email, password, repo: BookYourTaxi.Repo) do
      {:ok, conn} ->
        conn
        |> put_flash(:info, "Welcome #{user.full_name}")
        |> redirect(to: Routes.user_path(conn, :show, user))
      {:error, _reason, conn} ->
        conn
        |> put_flash(:error, "Bad credentials")
        |> render("new.html")
    end
  end
end
