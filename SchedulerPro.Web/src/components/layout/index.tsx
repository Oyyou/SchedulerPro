import React from "react";
import { Header } from "components";
import styles from "./layout.module.scss";

type LayoutProps = {
  children?: React.ReactNode;
}

const Layout = ({ children }: LayoutProps) => {
  return (
    <div className="app">
      <div className="app-body">
        <Header />
        <div className={styles.mainOuterContainer}>
          <main className={styles.mainContainer}>
            {children}
          </main>
        </div>
      </div>
    </div>
  );
};

export default Layout;
