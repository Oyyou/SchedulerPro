import { Outlet } from "react-router-dom";
import { AppContextProvider } from "providers/appContextProvider";
import { Layout } from "components";

const Root = () => {
  return (
    <AppContextProvider>
      <Layout>
        <Outlet />
      </Layout>
    </AppContextProvider>
  );
};

export default Root;