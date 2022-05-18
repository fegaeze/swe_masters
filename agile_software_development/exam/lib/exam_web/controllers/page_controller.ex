defmodule ExamWeb.PageController do
  use ExamWeb, :controller

  def index(conn, _params) do
    redirect(conn, to: Routes.loan_path(conn, :new))
  end
end
