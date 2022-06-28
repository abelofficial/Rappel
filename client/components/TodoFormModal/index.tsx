import React, { useState } from "react";
import { Form, Formik } from "formik";
import {
  initialValues,
  validationSchema,
  fieldNames,
  fieldLabels,
} from "./todoFormProps";
import { CreateTodoCommand } from "../../types";
import {
  Button,
  Card,
  Grid,
  Modal,
  Text,
  Tooltip,
  useTheme,
} from "@nextui-org/react";
import TextField, { TextAreaField } from "../TextField";
import { AddRoundedIconButton } from "../Buttons";

export interface TodoFormModalProps {
  propsValues?: CreateTodoCommand;
  actionButton?: JSX.Element;
  buttonTitle: string;
  title: string;

  onSubmit: (values: CreateTodoCommand) => Promise<void>;
}

const Index = ({
  title,
  propsValues,
  actionButton,
  buttonTitle,
  onSubmit,
}: TodoFormModalProps) => {
  const { theme } = useTheme();
  const [visible, setVisible] = React.useState(false);
  const [errors, setErrors] = useState<string[] | undefined>();

  const onSubmitHandler = async (values: CreateTodoCommand) => {
    try {
      await onSubmit(values);
      setVisible(false);
    } catch (e: any) {
      console.log("Error: ", e);
      setErrors(e?.response?.data?.errors);
    }
  };

  const handler = () => setVisible(true);

  const closeHandler = () => {
    setVisible(false);
  };

  const displayErrors = () => (
    <Tooltip content='' contentColor='error'>
      <Grid.Container alignItems='center' gap={1}>
        {errors?.map((e) => (
          <Grid key={e} xs={12}>
            <Text color='error'>{e}</Text>
          </Grid>
        ))}
      </Grid.Container>
    </Tooltip>
  );
  return (
    <Grid.Container
      justify='center'
      gap={2}
      css={{ backgroundColor: theme?.colors.backgroundContrast }}
    >
      {actionButton ? (
        <div onClick={handler}>{actionButton}</div>
      ) : (
        <Grid
          css={{
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
          }}
          onClick={handler}
        >
          <AddRoundedIconButton iconWidth={36} iconHeight={36} />
          <Text h4>Add new task</Text>
        </Grid>
      )}

      <Modal
        noPadding
        closeButton
        blur
        aria-labelledby='modal-title'
        open={visible}
        onClose={closeHandler}
        css={{ margin: "$10" }}
      >
        <Modal.Header>
          <Text id='modal-title' size={18}>
            {title}
          </Text>
        </Modal.Header>
        <Modal.Body>
          <Formik
            initialValues={propsValues ? propsValues : initialValues}
            onSubmit={onSubmitHandler}
            validationSchema={validationSchema}
          >
            {({ isSubmitting }) => (
              <Form>
                <Card variant='bordered'>
                  <Grid.Container
                    gap={1}
                    justify='center'
                    alignItems='center'
                    css={{
                      backgroundColor: theme?.colors.primaryShadow,
                      border: theme?.borderWeights.light,
                      boxShadow: theme?.shadows.lg,
                      padding: theme?.space.lg,
                    }}
                  >
                    {errors && displayErrors()}
                    <TextField
                      type='text'
                      name={fieldNames.title}
                      label={fieldLabels.title}
                      fullWidth
                    />
                    <TextAreaField
                      type='password'
                      name={fieldNames.description}
                      label={fieldLabels.description}
                      placeholder='Enter your amazing ideas.'
                      fullWidth
                    />

                    <Grid xs={12}>
                      <Button
                        type='submit'
                        color='success'
                        disabled={isSubmitting}
                      >
                        {buttonTitle}
                      </Button>
                    </Grid>
                  </Grid.Container>
                </Card>
              </Form>
            )}
          </Formik>
        </Modal.Body>
      </Modal>
    </Grid.Container>
  );
};

export default Index;
