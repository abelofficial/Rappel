import React from "react";
import { Form, Formik, FormikHelpers } from "formik";
import {
  initialValues,
  validationSchema,
  fieldNames,
  fieldLabels,
} from "./registerProps";
import { RegisterUserRequest } from "../../types";
import { Button, Grid, useTheme } from "@nextui-org/react";
import TextField from "../TextField";

const Index = () => {
  const { theme } = useTheme();
  const onSubmitHandler = async (
    values: RegisterUserRequest,
    actions: FormikHelpers<RegisterUserRequest>
  ) => {
    console.log("Values: " + values + "\n action: " + actions);
  };
  return (
    <Formik
      initialValues={initialValues}
      onSubmit={async (values, actions) => onSubmitHandler(values, actions)}
      validationSchema={validationSchema}
    >
      {({ errors, touched, isSubmitting }) => (
        <Form>
          <Grid.Container
            gap={1}
            justify='center'
            alignItems='center'
            css={{
              backgroundColor: theme?.colors.primaryShadow,
              border: theme?.borderWeights.light,
              boxShadow: theme?.shadows.lg,
            }}
          >
            <TextField
              type='text'
              name={fieldNames.firstName}
              label={fieldLabels.firstName}
              fullWidth
            />

            <TextField
              type='text'
              name={fieldNames.lastName}
              label={fieldLabels.lastName}
              fullWidth
            />
            <TextField
              type='email'
              name={fieldNames.email}
              label={fieldLabels.email}
              fullWidth
            />
            <TextField
              type='text'
              name={fieldNames.username}
              label={fieldLabels.username}
              fullWidth
            />
            <TextField
              type='password'
              name={fieldNames.password}
              label={fieldLabels.password}
              fullWidth
            />

            <TextField
              type='password'
              name={fieldNames.confirmPassword}
              label={fieldLabels.confirmPassword}
              fullWidth
            />

            <Grid xs={12}>
              <Button type='submit' color='success' disabled={isSubmitting}>
                Submit
              </Button>
            </Grid>
          </Grid.Container>
        </Form>
      )}
    </Formik>
  );
};

export default Index;